using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace JongQServiceAPI.Models
{
    public class QueueRepository
    {
        private QueueDbContext context = new QueueDbContext();

        public IEnumerable<BonChonTable> BonChonTableEntity { get { return context.BonChonTableEntity; } }
        public IEnumerable<BonChonTableHistory> BonChonTableHistoryEntity { get { return context.BonChonTableHistoryEntity; } }
        public IEnumerable<AfterYouTable> AfterYouTableEntity { get { return context.AfterYouTableEntity; } }
        public IEnumerable<AfterYouTableHistory> AfterYouTableHistoryEntity { get { return context.AfterYouTableHistoryEntity; } }
        public IEnumerable<BarBQPlazaTable> BarBQPlazaTableEntity { get { return context.BarBQPlazaTableEntity; } }
        public IEnumerable<EatAmAreTable> EatAmAreTableEntity { get { return context.EatAmAreTableEntity; } }
        public IEnumerable<ResStatus> ResStatusEntity { get { return context.ResStatusEntity; } }

        public async Task<int> StartQueueSystem(int Id)
        {
            
            ResStatus dbEntry = context.ResStatusEntity.Find(Id);
            if (dbEntry != null) 
            {
                dbEntry.QueueStatus = true;
            }

            return await context.SaveChangesAsync();
        }

        public async Task<int> StopQueueSystem(int Id)
        {

            ResStatus dbEntry = context.ResStatusEntity.Find(Id);
            if (dbEntry != null)
            {
                dbEntry.QueueStatus = false;
            }

            return await context.SaveChangesAsync();
        }

        /******* ADD QUEUE ********/
        public async Task<int> AddQueueBonchon(BonChonTable BonChonQueue)
        {
            context.BonChonTableEntity.Add(BonChonQueue);
            return await context.SaveChangesAsync();
        }

        /*---------------------------*/
        public async Task<int> AddQueueAfterYou(AfterYouTable AfterYouQueue)
        {
            context.AfterYouTableEntity.Add(AfterYouQueue);
            return await context.SaveChangesAsync();
        }

        /*---------------------------*/
        public async Task<int> AddQueueBarBQPlaza(BarBQPlazaTable BarBQPlazaQueue)
        {
            context.BarBQPlazaTableEntity.Add(BarBQPlazaQueue);
            return await context.SaveChangesAsync();
        }

        /*---------------------------*/
        public async Task<int> AddQueueEatAmAre(EatAmAreTable EatAmAreQueue)
        {
            context.EatAmAreTableEntity.Add(EatAmAreQueue);
            return await context.SaveChangesAsync();
        }

        /****** END ADD QUEUE ******/
        


        /******* QUEUE ACCEPT ******/

        public async Task<int> QueueAcceptBonChon(int Id)
        {
            BonChonTable dbEntry = context.BonChonTableEntity.Find(Id);
            if (dbEntry != null)
            {
                dbEntry.Status = "accept";
                dbEntry.QueueCheck = true;
                dbEntry.ArriveTime = DateTime.Now;
            }
            return await context.SaveChangesAsync();
        }

        /*---------------------------*/

        public async Task<int> QueueAcceptAfterYou(int Id)
        {
            AfterYouTable dbEntry = context.AfterYouTableEntity.Find(Id);
            if (dbEntry != null)
            {
                dbEntry.Status = "accept";
                dbEntry.QueueCheck = true;
                dbEntry.ArriveTime = DateTime.Now;
            }
            return await context.SaveChangesAsync();
        }

        /*---------------------------*/

        public async Task<int> QueueAcceptBarBQPlaza(int Id)
        {
            BarBQPlazaTable dbEntry = context.BarBQPlazaTableEntity.Find(Id);
            if (dbEntry != null)
            {
                dbEntry.Status = "accept";
                dbEntry.QueueCheck = true;
                dbEntry.ArriveTime = DateTime.Now;
            }
            return await context.SaveChangesAsync();
        }

        /*---------------------------*/

        public async Task<int> QueueAcceptEatAmAre(int Id)
        {
            EatAmAreTable dbEntry = context.EatAmAreTableEntity.Find(Id);
            if (dbEntry != null)
            {
                dbEntry.Status = "accept";
                dbEntry.QueueCheck = true;
                dbEntry.ArriveTime = DateTime.Now;
            }
            return await context.SaveChangesAsync();
        }


        /******* END QUEUE ACCEPT *****/

        /******* QUEUE SKIP ******/

        public async Task<int> QueueSkipBonChon(int Id)
        {
            BonChonTable dbEntry = context.BonChonTableEntity.Find(Id);
            if (dbEntry != null)
            {
                dbEntry.Status = "skip";
                dbEntry.QueueCheck = true;
                dbEntry.ArriveTime = DateTime.Now;
            }
            return await context.SaveChangesAsync();
        }

        /*---------------------------*/

        public async Task<int> QueueSkipAfterYou(int Id)
        {
            AfterYouTable dbEntry = context.AfterYouTableEntity.Find(Id);
            if (dbEntry != null)
            {
                dbEntry.Status = "skip";
                dbEntry.QueueCheck = true;
                dbEntry.ArriveTime = DateTime.Now;
            }
            return await context.SaveChangesAsync();
        }

        /*---------------------------*/

        public async Task<int> QueueSkipBarBQPlaza(int Id)
        {
            BarBQPlazaTable dbEntry = context.BarBQPlazaTableEntity.Find(Id);
            if (dbEntry != null)
            {
                dbEntry.Status = "skip";
                dbEntry.QueueCheck = true;
                dbEntry.ArriveTime = DateTime.Now;
            }
            return await context.SaveChangesAsync();
        }

        /*---------------------------*/

        public async Task<int> QueueSkipEatAmAre(int Id)
        {
            EatAmAreTable dbEntry = context.EatAmAreTableEntity.Find(Id);
            if (dbEntry != null)
            {
                dbEntry.Status = "skip";
                dbEntry.QueueCheck = true;
                dbEntry.ArriveTime = DateTime.Now;
            }
            return await context.SaveChangesAsync();
        }


        /******* END QUEUE SKIP *****/

        /******* Change queue Waiting to Standby *******/
        public async Task<int> ChangeWaitingBonChon(int Id)
        {
            BonChonTable dbEntry = context.BonChonTableEntity.Find(Id);
            if (dbEntry != null)
            {
                dbEntry.Status = "standby";
            }
            return await context.SaveChangesAsync();
        }

        /*---------------------------*/

        public async Task<int> ChangeWaitingAfterYou(int Id)
        {
            AfterYouTable dbEntry = context.AfterYouTableEntity.Find(Id);
            if (dbEntry != null)
            {
                dbEntry.Status = "standby";
            }
            return await context.SaveChangesAsync();
        }

        /*---------------------------*/

        public async Task<int> ChangeWaitingBarBQPlaza(int Id)
        {
            BarBQPlazaTable dbEntry = context.BarBQPlazaTableEntity.Find(Id);
            if (dbEntry != null)
            {
                dbEntry.Status = "standby";
            }
            return await context.SaveChangesAsync();
        }

        /*---------------------------*/

        public async Task<int> ChangeWaitingEatAmAre(int Id)
        {
            EatAmAreTable dbEntry = context.EatAmAreTableEntity.Find(Id);
            if (dbEntry != null)
            {
                dbEntry.Status = "standby";
            }
            return await context.SaveChangesAsync();
        }


        /******* End Change queue Waiting to Standby *******/

        /******* Change queue Waiting to Cancel *******/

        public async Task<int> CancelQueueBonChon(int Id)
        {
            BonChonTable dbEntry = context.BonChonTableEntity.Find(Id);
            if (dbEntry != null)
            {
                dbEntry.Status = "cancel";
                dbEntry.QueueCheck = true;
                dbEntry.ArriveTime = DateTime.Now;
            }
            return await context.SaveChangesAsync();
        }

        /*---------------------------*/

        public async Task<int> CancelQueueAfterYou(int Id)
        {
            AfterYouTable dbEntry = context.AfterYouTableEntity.Find(Id);
            if (dbEntry != null)
            {
                dbEntry.Status = "cancel";
                dbEntry.QueueCheck = true;
                dbEntry.ArriveTime = DateTime.Now;
            }
            return await context.SaveChangesAsync();
        }

        /*---------------------------*/

        public async Task<int> CancelQueueBarBQPlaza(int Id)
        {
            BarBQPlazaTable dbEntry = context.BarBQPlazaTableEntity.Find(Id);
            if (dbEntry != null)
            {
                dbEntry.Status = "cancel";
                dbEntry.QueueCheck = true;
                dbEntry.ArriveTime = DateTime.Now;
            }
            return await context.SaveChangesAsync();
        }

        /*---------------------------*/

        public async Task<int> CancelQueueEatAmAre(int Id)
        {
            EatAmAreTable dbEntry = context.EatAmAreTableEntity.Find(Id);
            if (dbEntry != null)
            {
                dbEntry.Status = "cancel";
                dbEntry.QueueCheck = true;
                dbEntry.ArriveTime = DateTime.Now;
            }
            return await context.SaveChangesAsync();
        }



        /*******End Change queue Waiting to Cancel *******/

        


    }
}