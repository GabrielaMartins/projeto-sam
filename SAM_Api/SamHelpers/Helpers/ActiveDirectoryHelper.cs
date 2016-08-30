using System;
using System.IO;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices;

namespace SamHelpers
{
    public class ActiveDirectoryHelper
    {

        public string DC { get; private set; }

        public ActiveDirectoryHelper(string DC)
        {
            this.DC = DC;
        }

        public ActiveDirectoryUser GetUser(string username)
        {
            UserPrincipal user = null;
            using (var pc = new PrincipalContext(ContextType.Domain, DC))
            {
                user = UserPrincipal.FindByIdentity(pc, IdentityType.SamAccountName, DC + "\\" + username);
                if (user != null)
                {
                    DirectoryEntry de = user.GetUnderlyingObject() as DirectoryEntry;

                    var samAccountName = user.SamAccountName;
                    var name = user.DisplayName;
                    var email = user.EmailAddress;
                    var creationDate = (DateTime)de.Properties["msExchWhenMailboxCreated"].Value;
                    return new ActiveDirectoryUser(samAccountName, name, email, creationDate);
                }
            }

            return null;
        }

        public bool IsValidUser(string username, string password)
        {

            bool isValid = false;

            // create a "principal context" - e.g. your domain (could be machine, too)
            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, DC))
            {
                // validate the credentials
                isValid = pc.ValidateCredentials(username, password);
            }

            return isValid;
        }

        public List<ActiveDirectoryUser> GetAllUsers()
        {
            try
            {
                List<ActiveDirectoryUser> users = new List<ActiveDirectoryUser>();

                using (var context = new PrincipalContext(ContextType.Domain, "opus.local"))
                {
                    using (var searcher = new PrincipalSearcher(new UserPrincipal(context)))
                    {

                        GroupPrincipal grp = GroupPrincipal.FindByIdentity(context, IdentityType.Name, "staff");
                        if (grp != null)
                        {

                            //var sw = System.IO.File.AppendText(@"C:\\Users\\jesley.OPUS\\Desktop\\log1.txt");
                            foreach (Principal result in grp.GetMembers(true))
                            {

                                var user = GetUser(result.SamAccountName);
                                if (user != null)
                                    users.Add(user);
                            }
                        }
                    }
                }

                return users;
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public void ListUserProperties(string username)
        {

            using (var context = new PrincipalContext(ContextType.Domain, Environment.UserDomainName))
            {
                using (var searcher = new PrincipalSearcher(new UserPrincipal(context)))
                {
                    foreach (var result in searcher.FindAll())
                    {
                        DirectoryEntry de = result.GetUnderlyingObject() as DirectoryEntry;
                        if ((string)de.Properties["SamAccountName"].Value == username)
                        {
                            //Console.WriteLine("First Name: " + 
                            //de.Properties["givenName"].Value);
                            //Console.WriteLine("Last Name : " + 
                            //de.Properties["sn"].Value);
                            //Console.WriteLine("SAM account name   : " + 
                            //de.Properties["samAccountName"].Value);
                            //Console.WriteLine("User principal name: " + 
                            //de.Properties["userPrincipalName"].Value);
                            Console.WriteLine();
                            PropertyCollection pc = de.Properties;
                            using (FileStream fs = new FileStream(@"C:\Users\jesley.OPUS\Desktop\log.txt", FileMode.Append, FileAccess.Write))
                            {
                                using (StreamWriter sw = new StreamWriter(fs))
                                {

                                    sw.WriteLine("**** Properties for user: " + username + " ****");
                                    foreach (PropertyValueCollection col in pc)
                                    {
                                        sw.WriteLine(col.PropertyName + " : " + col.Value);
                                        sw.WriteLine();
                                    }
                                    sw.WriteLine();
                                }
                            }
                            
                        }
                    }
                }
            }
        }
    }

}
