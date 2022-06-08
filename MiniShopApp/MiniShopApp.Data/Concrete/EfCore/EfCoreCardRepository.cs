using Microsoft.EntityFrameworkCore;
using MiniShopApp.Data.Abstract;
using MiniShopApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniShopApp.Data.Concrete.EfCore
{
    public class EfCoreCardRepository : EfCoreGenericRepository<Card, MiniShopContext>, ICardRepository
    {
        public void DeleteFromCard(int cardId, int productId)
        {
            using (var context = new MiniShopContext())
            {
                var query = @"DELETE FROM CardItems WHERE CardId=@p0 AND ProductId=@p1";
                context.Database.ExecuteSqlRaw(query,cardId,productId);
            }
        }

        public Card GetCardByUserId(string userId)
        {
            using (var context = new MiniShopContext())
            {
                var a= context.Cards.Include(i => i.CardItems).ThenInclude(i => i.Product).FirstOrDefault(i => i.UserId == userId);
                return a;
            }
        }
        public override void Update(Card entity)
        {
            using (var context = new MiniShopContext())
            {
                context.Cards.Update(entity);
                context.SaveChanges();
            }
            base.Update(entity);
        }
    }
}
