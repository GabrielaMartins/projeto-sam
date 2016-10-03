using AutoMapper;
using Opus.DataBaseEnvironment;
using MessageSystem.Erro;
using SamApiModels.Item;
using SamDataBase.Model;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace SamServices.Services
{
    public static class ItemServices
    {
        public static List<ItemViewModel> RecuperaItens()
        {
            using(var itemRep = DataAccess.Instance.GetItemRepository())
            {
                var itens = itemRep.GetAll().ToList();
                var itensViewModel = Mapper.Map<List<Item>, List<ItemViewModel>>(itens);

                //foreach(var item in itens)
                //{
                //    var usuarios = itemRep.RecuperaUsuariosQueFizeram(item.id);

                //    var itemViewModel = new ItemViewModel()
                //    {
                //        Categoria = Mapper.Map<Categoria, CategoriaViewModel>(item.Categoria),
                //        descricao = item.descricao,
                //        dificuldade = item.dificuldade,
                //        id = item.id,
                //        modificador = item.modificador,
                //        nome = item.nome,
                //        Usuarios = Mapper.Map<List<Usuario>, List<UsuarioViewModel>>(usuarios)
                //    };

                //    itensViewModel.Add(itemViewModel);
                //}

                return itensViewModel;
            }
        }

        public static void AtualizaItem(int id, UpdateItemViewModel item)
        {
            using (var itemRep = DataAccess.Instance.GetItemRepository())
            {
                var itemToBeUpdated = itemRep.Find(i => i.id == id).SingleOrDefault();
                if (itemToBeUpdated == null)
                {
                    throw new ErroEsperado(HttpStatusCode.NotFound, "Item Not Found", $"Item #{id} not found");
                }

                // map new values to our reference
                itemToBeUpdated = Mapper.Map(item, itemToBeUpdated);

                // add to entity context
                itemRep.Update(itemToBeUpdated);

                // commit changes
                itemRep.SubmitChanges();
            }
        }

        public static void DeleteItem(int id)
        {
            using (var itemRep = DataAccess.Instance.GetItemRepository())
            {
                itemRep.Delete(id);
                itemRep.SubmitChanges();
            }
        }

        public static void CriaItem(AddItemViewModel item)
        {
            using (var itemRep = DataAccess.Instance.GetItemRepository())
            {
                // map new values to our reference
                var newItem = Mapper.Map<AddItemViewModel, Item>(item);

                // add to entity context
                itemRep.Add(newItem);

                // commit changes
                itemRep.SubmitChanges();
            }
        }

        public static ItemViewModel Recupera(int id)
        {
            using (var itemRep = DataAccess.Instance.GetItemRepository())
            {
                var item = itemRep.Find(i => i.id == id).SingleOrDefault();
                var itemViewModel = Mapper.Map<Item, ItemViewModel>(item);
                return itemViewModel;
            }
        }
    }
}
