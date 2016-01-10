using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
//using System.Web.Mvc;
using JongQServiceAPI.Models;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Web.Http.ModelBinding;
using JongQServiceAPI.CustomResponseContent;

namespace JongQServiceAPI.Controllers
{
    public class QueueServiceController : ApiController
    {
        private QueueRepository QueueRepository { get; set; }
        private UserRepository UserRepository { get; set; }
        // GET: QueueService
        public QueueServiceController()
        {
            QueueRepository = new QueueRepository();
            UserRepository = new UserRepository();
        }


        /********************** TEST QUEUE STATUS  **************/
        [HttpGet]
        [ActionName("AllResBranch")]
        public IEnumerable<ResStatus> Test()
        {
            return QueueRepository.ResStatusEntity;
        }

        /*********Restaurant Call Service************/

        [HttpPost]
        [ActionName("StartQueueSystem")]
        public async Task<StartQueueSystemResponse> StartQueueSystem(StartQueueSystemData data)
        {
            StartQueueSystemResponse resp = new StartQueueSystemResponse();
            var searchId = (from o in QueueRepository.ResStatusEntity
                            where o.ResName == data.ResName && o.ResBranch == data.ResBranch
                            select o).ToList();
            int Id = 0;
            foreach (ResStatus getId in searchId)
            {
                Id = getId.Id;
            }
            await QueueRepository.StartQueueSystem(Id);
            resp.SystemStatus = true;
            return resp;       
        }

        [HttpPost]
        [ActionName("StopQueueSystem")]
        public async Task<StartQueueSystemResponse> StopQueueSystem(StartQueueSystemData data)
        {
            StartQueueSystemResponse resp = new StartQueueSystemResponse();
            var searchId = (from o in QueueRepository.ResStatusEntity
                            where o.ResName == data.ResName && o.ResBranch == data.ResBranch
                            select o).ToList();
            int Id = 0;
            foreach (ResStatus getId in searchId)
            {
                Id = getId.Id;
            }
            await QueueRepository.StopQueueSystem(Id);
            resp.SystemStatus = true;
            return resp;
        }

        [HttpPost]
        [ActionName("GetQueueList")]
        public GetQueueListResponse GetQueueList(GetQueueListData data)
        {
            GetQueueListResponse resp = new GetQueueListResponse();
            List<QueueList> listQ = new List<QueueList>();

            if(data.ResName == "BonChon")
            { 

                var waitingQ = (from o in QueueRepository.BonChonTableEntity
                                where o.ResBranch == data.ResBranch && o.QueueType == data.QueueType && o.QueueCheck == false
                                select o).Count();
                resp.TotalQueueWait = waitingQ;

                var queryQ = (from o in QueueRepository.BonChonTableEntity
                              where o.ResBranch == data.ResBranch && o.QueueType == data.QueueType && o.QueueCheck == false
                              select o).ToList();
                foreach (BonChonTable s in queryQ)
                {
                    QueueList temp = new QueueList();
                    temp.UserId = s.UserId;
                    temp.QueueNum = s.QueueNum;
                    temp.QueueType = s.QueueType;
                    temp.Nickname = s.Nickname;
                    temp.QueueCheck = s.QueueCheck;
                    temp.Status = s.Status;

                    listQ.Add(temp);
                }
                resp.QueueList = listQ;


            }

            /*********************************************************/

            if(data.ResName == "AfterYou")
            {
                var waitingQ = (from o in QueueRepository.AfterYouTableEntity
                                where o.ResBranch == data.ResBranch && o.QueueType == data.QueueType && o.QueueCheck == false
                                select o).Count();
                resp.TotalQueueWait = waitingQ;

                var queryQ = (from o in QueueRepository.AfterYouTableEntity
                              where o.ResBranch == data.ResBranch && o.QueueType == data.QueueType && o.QueueCheck == false
                              select o).ToList();
                foreach (AfterYouTable s in queryQ)
                {
                    QueueList temp = new QueueList();
                    temp.UserId = s.UserId;
                    temp.QueueNum = s.QueueNum;
                    temp.QueueType = s.QueueType;
                    temp.Nickname = s.Nickname;
                    temp.QueueCheck = s.QueueCheck;
                    temp.Status = s.Status;

                    listQ.Add(temp);
                }
                resp.QueueList = listQ;

            }

            /*********************************************************/

            if (data.ResName == "BarBQPlaza")
            {
                var waitingQ = (from o in QueueRepository.BarBQPlazaTableEntity
                                where o.ResBranch == data.ResBranch && o.QueueType == data.QueueType && o.QueueCheck == false
                                select o).Count();
                resp.TotalQueueWait = waitingQ;

                var queryQ = (from o in QueueRepository.BarBQPlazaTableEntity
                              where o.ResBranch == data.ResBranch && o.QueueType == data.QueueType && o.QueueCheck == false
                              select o).ToList();
                foreach (BarBQPlazaTable s in queryQ)
                {
                    QueueList temp = new QueueList();
                    temp.UserId = s.UserId;
                    temp.QueueNum = s.QueueNum;
                    temp.QueueType = s.QueueType;
                    temp.Nickname = s.Nickname;
                    temp.QueueCheck = s.QueueCheck;
                    temp.Status = s.Status;

                    listQ.Add(temp);
                }
                resp.QueueList = listQ;

            }

            /*********************************************************/

            if (data.ResName == "EatAmAre")
            {
                var waitingQ = (from o in QueueRepository.EatAmAreTableEntity
                                where o.ResBranch == data.ResBranch && o.QueueType == data.QueueType && o.QueueCheck == false
                                select o).Count();
                resp.TotalQueueWait = waitingQ;

                var queryQ = (from o in QueueRepository.EatAmAreTableEntity
                              where o.ResBranch == data.ResBranch && o.QueueType == data.QueueType && o.QueueCheck == false
                              select o).ToList();
                foreach (EatAmAreTable s in queryQ)
                {
                    QueueList temp = new QueueList();
                    temp.UserId = s.UserId;
                    temp.QueueNum = s.QueueNum;
                    temp.QueueType = s.QueueType;
                    temp.Nickname = s.Nickname;
                    temp.QueueCheck = s.QueueCheck;
                    temp.Status = s.Status;

                    listQ.Add(temp);
                }
                resp.QueueList = listQ;

            }

            return resp;

        }

        [HttpPost]
        [ActionName("ReservingAnonymous")]
        public async Task<ReserveResponse> ReservingAnonymous(ReserveData data)
        {
            ReserveResponse s = new ReserveResponse();

            if (data.ResName == "BonChon")
            {
                var qCount = (from o in QueueRepository.BonChonTableEntity
                              where o.ResBranch == data.ResBranch && o.QueueType == data.QueueType && o.QueueCheck == false
                              select o).Count();
                var nextQCount = qCount + 1;

                

                if (data.CurrentUserQueue != qCount)
                {
                    s.Error = true;
                    s.MsgEng = "Current Queue Has Been Change, Currently is " + nextQCount + data.QueueType;
                    s.MsgThai = "คิวปัจจุบันของคุณมีการเปลี่ยนแปลง ปัจจุบันคือคิวที่ " + nextQCount + data.QueueType;
                    return s;
                }
                else
                {
                    /*********** Not check panalty reserve yet ********/

                    BonChonTable reserved = new BonChonTable();
                    if (qCount == 0)
                    {
                        reserved.Status = "standby";
                        reserved.QueueNum = 1;
                    }
                    else
                    {
                        BonChonTable lastQ = (from o in QueueRepository.BonChonTableEntity
                                              where o.ResBranch == data.ResBranch && o.QueueType == data.QueueType && o.QueueCheck == false
                                              select o).OrderByDescending(o => o.QueueNum).FirstOrDefault();

                        int sendQueue = lastQ.QueueNum + 1;
                        reserved.QueueNum = sendQueue;
                        reserved.Status = "waiting";
                        //var tmp = (from o in QueueRepository.BonChonTableEntity
                        //           where o.ResBranch == data.ResBranch && o.QueueType == data.QueueType && o.QueueNum == qCount
                        //           select o).ToList();
                        //foreach (BonChonTable a in tmp)
                        //{
                        //    if (a.QueueCheck == false)
                        //    {
                        //        reserved.Status = "waiting";
                        //    }
                        //    else
                        //    {
                        //        reserved.Status = "standby";
                        //    }
                        //}
                    }


                    reserved.Id = 0;
                    //User ID = 0 mean anonymouse reserving
                    reserved.UserId = 0;
                    reserved.Nickname = data.Nickname;
                    
                    reserved.ReserveTime = DateTime.Now;
                    reserved.ArriveTime = null;

                    reserved.QueueType = data.QueueType;
                    reserved.QueueCheck = false;
                    reserved.ResBranch = data.ResBranch;
                    reserved.ReserveLocation = "none";
                    await QueueRepository.AddQueueBonchon(reserved);

                    s.Error = false;
                    s.MsgEng = "Reserved";
                    s.MsgThai = "ทำการจองเรียบร้อย";

                    return s;
                }

            }

            /*********************************************************/

            if (data.ResName == "AfterYou")
            {
                var qCount = (from o in QueueRepository.AfterYouTableEntity
                              where o.ResBranch == data.ResBranch && o.QueueType == data.QueueType && o.QueueCheck == false
                              select o).Count();
                var nextQCount = qCount + 1;



                if (data.CurrentUserQueue != qCount)
                {
                    s.Error = true;
                    s.MsgEng = "Current Queue Has Been Change, Currently is " + nextQCount + data.QueueType;
                    s.MsgThai = "คิวปัจจุบันของคุณมีการเปลี่ยนแปลง ปัจจุบันคือคิวที่ " + nextQCount + data.QueueType;
                    return s;
                }
                else
                {
                    /*********** Not check panalty reserve yet ********/

                    AfterYouTable reserved = new AfterYouTable();
                    if (qCount == 0)
                    {
                        reserved.Status = "standby";
                        reserved.QueueNum = 1;
                    }
                    else
                    {
                        AfterYouTable lastQ = (from o in QueueRepository.AfterYouTableEntity
                                               where o.ResBranch == data.ResBranch && o.QueueType == data.QueueType && o.QueueCheck == false
                                               select o).OrderByDescending(o => o.QueueNum).FirstOrDefault();

                        int sendQueue = lastQ.QueueNum + 1;
                        reserved.QueueNum = sendQueue;
                        reserved.Status = "waiting";
                    }


                    reserved.Id = 0;
                    //User ID = 0 mean anonymouse reserving
                    reserved.UserId = 0;
                    reserved.Nickname = data.Nickname;
                    reserved.ReserveTime = DateTime.Now;
                    reserved.ArriveTime = null;

                    reserved.QueueType = data.QueueType;
                    reserved.QueueCheck = false;
                    reserved.ResBranch = data.ResBranch;
                    reserved.ReserveLocation = "none";
                    await QueueRepository.AddQueueAfterYou(reserved);

                    s.Error = false;
                    s.MsgEng = "Reserved";
                    s.MsgThai = "ทำการจองเรียบร้อย";

                    return s;
                }
            }

            /*********************************************************/

            if (data.ResName == "BarBQPlaza")
            {
                var qCount = (from o in QueueRepository.BarBQPlazaTableEntity
                              where o.ResBranch == data.ResBranch && o.QueueType == data.QueueType && o.QueueCheck == false
                              select o).Count();
                var nextQCount = qCount + 1;

                

                if (data.CurrentUserQueue != qCount)
                {
                    s.Error = true;
                    s.MsgEng = "Current Queue Has Been Change, Currently is " + nextQCount + data.QueueType;
                    s.MsgThai = "คิวปัจจุบันของคุณมีการเปลี่ยนแปลง ปัจจุบันคือคิวที่ " + nextQCount + data.QueueType;
                    return s;
                }
                else
                {
                    /*********** Not check panalty reserve yet ********/

                    BarBQPlazaTable reserved = new BarBQPlazaTable();
                    if (qCount == 0)
                    {
                        reserved.Status = "standby";
                        reserved.QueueNum = 1;
                    }
                    else
                    {
                        BarBQPlazaTable lastQ = (from o in QueueRepository.BarBQPlazaTableEntity
                                                 where o.ResBranch == data.ResBranch && o.QueueType == data.QueueType && o.QueueCheck == false
                                                 select o).OrderByDescending(o => o.QueueNum).FirstOrDefault();

                        int sendQueue = lastQ.QueueNum + 1;
                        reserved.QueueNum = sendQueue;
                        reserved.Status = "waiting";
                    }


                    reserved.Id = 0;
                    //User ID = 0 mean anonymouse reserving
                    reserved.UserId = 0;
                    reserved.Nickname = data.Nickname;
                    reserved.ReserveTime = DateTime.Now;
                    reserved.ArriveTime = null;

                    reserved.QueueType = data.QueueType;
                    reserved.QueueCheck = false;
                    reserved.ResBranch = data.ResBranch;
                    reserved.ReserveLocation = "none";
                    await QueueRepository.AddQueueBarBQPlaza(reserved);

                    s.Error = false;
                    s.MsgEng = "Reserved";
                    s.MsgThai = "ทำการจองเรียบร้อย";

                    return s;
                }
            }

            /*********************************************************/

            if (data.ResName == "EatAmAre")
            {
                var qCount = (from o in QueueRepository.EatAmAreTableEntity
                              where o.ResBranch == data.ResBranch && o.QueueType == data.QueueType && o.QueueCheck == false
                              select o).Count();
                var nextQCount = qCount + 1;



                if (data.CurrentUserQueue != qCount)
                {
                    s.Error = true;
                    s.MsgEng = "Current Queue Has Been Change, Currently is " + nextQCount + data.QueueType;
                    s.MsgThai = "คิวปัจจุบันของคุณมีการเปลี่ยนแปลง ปัจจุบันคือคิวที่ " + nextQCount + data.QueueType;
                    return s;
                }
                else
                {
                    /*********** Not check panalty reserve yet ********/

                    EatAmAreTable reserved = new EatAmAreTable();
                    if (qCount == 0)
                    {
                        reserved.Status = "standby";
                        reserved.QueueNum = 1;
                    }
                    else
                    {
                        EatAmAreTable lastQ = (from o in QueueRepository.EatAmAreTableEntity
                                                 where o.ResBranch == data.ResBranch && o.QueueType == data.QueueType && o.QueueCheck == false
                                                 select o).OrderByDescending(o => o.QueueNum).FirstOrDefault();

                        int sendQueue = lastQ.QueueNum + 1;
                        reserved.QueueNum = sendQueue;
                        reserved.Status = "waiting";
                    }


                    reserved.Id = 0;
                    //User ID = 0 mean anonymouse reserving
                    reserved.UserId = 0;
                    reserved.Nickname = data.Nickname;
                    reserved.ReserveTime = DateTime.Now;
                    reserved.ArriveTime = null;

                    reserved.QueueType = data.QueueType;
                    reserved.QueueCheck = false;
                    reserved.ResBranch = data.ResBranch;
                    reserved.ReserveLocation = "none";
                    await QueueRepository.AddQueueEatAmAre(reserved);

                    s.Error = false;
                    s.MsgEng = "Reserved";
                    s.MsgThai = "ทำการจองเรียบร้อย";

                    return s;
                }
            }


            return s;
        }

        [HttpPost]
        [ActionName("AcceptQueue")]
        public async Task<AcceptQueueResponse> AcceptQueue(AcceptQueueData data)
        {
            /********** Don't forget Push Notification *****/
            AcceptQueueResponse resp = new AcceptQueueResponse();
            //re reserve point
            await UserRepository.UpdateReservePoint(data.UserId, 3);

            if (data.ResName == "BonChon")
            {
                // get id to change status standby --> accept
                var findId = (from o in QueueRepository.BonChonTableEntity
                                   where o.ResBranch == data.ResBranch && o.UserId == data.UserId && data.ResName == data.ResName && o.QueueType == data.QueueType && o.Status == "standby"
                                   select o).ToList();
                int Id = 0;
                foreach(BonChonTable d in findId) 
                {
                    Id = d.Id;
                }
                await QueueRepository.QueueAcceptBonChon(Id);

                // change follow queue waiting --> standby
                BonChonTable findQueueWaitingId = (from o in QueueRepository.BonChonTableEntity
                                                   where o.ResBranch == data.ResBranch && o.QueueType == data.QueueType && o.QueueCheck == false
                                                   select o).FirstOrDefault();
                if (findQueueWaitingId != null)
                {
                    int followId = findQueueWaitingId.Id;
                    await QueueRepository.ChangeWaitingBonChon(followId);
                }

            }

            /*******************************************************/

            if (data.ResName == "AfterYou")
            {
                // get id to change status standby --> accept
                var findId = (from o in QueueRepository.AfterYouTableEntity
                              where o.ResBranch == data.ResBranch && o.UserId == data.UserId && data.ResName == data.ResName && o.QueueType == data.QueueType && o.Status == "standby"
                              select o).ToList();
                int Id = 0;
                foreach (AfterYouTable d in findId)
                {
                    Id = d.Id;
                }
                await QueueRepository.QueueAcceptAfterYou(Id);

                // change follow queue waiting --> standby
                AfterYouTable findQueueWaitingId = (from o in QueueRepository.AfterYouTableEntity
                                                   where o.ResBranch == data.ResBranch && o.QueueType == data.QueueType && o.QueueCheck == false
                                                   select o).FirstOrDefault();
                if (findQueueWaitingId != null)
                {
                    int followId = findQueueWaitingId.Id;
                    await QueueRepository.ChangeWaitingAfterYou(followId);
                }
            }

            /*****************************************************/

            if (data.ResName == "BarBQPlaza")
            {
                // get id to change status standby --> accept
                var findId = (from o in QueueRepository.BarBQPlazaTableEntity
                              where o.ResBranch == data.ResBranch && o.UserId == data.UserId && data.ResName == data.ResName && o.QueueType == data.QueueType && o.Status == "standby"
                              select o).ToList();
                int Id = 0;
                foreach (BarBQPlazaTable d in findId)
                {
                    Id = d.Id;
                }
                await QueueRepository.QueueAcceptBarBQPlaza(Id);

                // change follow queue waiting --> standby
                BarBQPlazaTable findQueueWaitingId = (from o in QueueRepository.BarBQPlazaTableEntity
                                                   where o.ResBranch == data.ResBranch && o.QueueType == data.QueueType && o.QueueCheck == false
                                                   select o).FirstOrDefault();
                if (findQueueWaitingId != null)
                {
                    int followId = findQueueWaitingId.Id;
                    await QueueRepository.ChangeWaitingBarBQPlaza(followId);
                }
            }

            /*****************************************************/

            if (data.ResName == "EatAmAre")
            {
                // get id to change status standby --> accept
                var findId = (from o in QueueRepository.EatAmAreTableEntity
                              where o.ResBranch == data.ResBranch && o.UserId == data.UserId && data.ResName == data.ResName && o.QueueType == data.QueueType && o.Status == "standby"
                              select o).ToList();
                int Id = 0;
                foreach (EatAmAreTable d in findId)
                {
                    Id = d.Id;
                }
                await QueueRepository.QueueAcceptEatAmAre(Id);

                // change follow queue waiting --> standby
                EatAmAreTable findQueueWaitingId = (from o in QueueRepository.EatAmAreTableEntity
                                                      where o.ResBranch == data.ResBranch && o.QueueType == data.QueueType && o.QueueCheck == false
                                                      select o).FirstOrDefault();
                if (findQueueWaitingId != null)
                {
                    int followId = findQueueWaitingId.Id;
                    await QueueRepository.ChangeWaitingEatAmAre(followId);
                }
            }


            await UserRepository.ChangeIsReserveAsync(data.UserId);
            resp.Success = true;

            return resp;
        }

        [HttpPost]
        [ActionName("SkipQueue")]
        public async Task<SkipQueueResponse> SkipQueue(SkipQueueData data)
        {
            /********** Don't forget Push Notification *****/
            SkipQueueResponse resp = new SkipQueueResponse();
            //re reserve point
            await UserRepository.UpdateReservePoint(data.UserId, 3);

            if (data.ResName == "BonChon")
            {
                // get id to change status standby --> accept
                var findId = (from o in QueueRepository.BonChonTableEntity
                              where o.ResBranch == data.ResBranch && o.UserId == data.UserId && data.ResName == data.ResName && o.QueueType == data.QueueType && o.Status == "standby"
                              select o).ToList();
                int Id = 0;
                foreach (BonChonTable d in findId)
                {
                    Id = d.Id;
                }
                await QueueRepository.QueueSkipBonChon(Id);

                // change follow queue waiting --> standby
                // caution should ignore cancel status

                BonChonTable findQueueWaitingId = (from o in QueueRepository.BonChonTableEntity
                                                    where o.ResBranch == data.ResBranch && o.QueueType == data.QueueType && o.QueueCheck == false
                                                    select o).FirstOrDefault();
                if (findQueueWaitingId != null)
                {
                    int followId = findQueueWaitingId.Id;
                    await QueueRepository.ChangeWaitingBonChon(followId);
                }


            }

            /*******************************************************/

            if (data.ResName == "AfterYou")
            {
                // get id to change status standby --> accept
                var findId = (from o in QueueRepository.AfterYouTableEntity
                              where o.ResBranch == data.ResBranch && o.UserId == data.UserId && data.ResName == data.ResName && o.QueueType == data.QueueType && o.Status == "standby"
                              select o).ToList();
                int Id = 0;
                foreach (AfterYouTable d in findId)
                {
                    Id = d.Id;
                }
                await QueueRepository.QueueSkipAfterYou(Id);

                // change follow queue waiting --> standby
                // caution should ignore cancel status

                AfterYouTable findQueueWaitingId = (from o in QueueRepository.AfterYouTableEntity
                                                   where o.ResBranch == data.ResBranch && o.QueueType == data.QueueType && o.QueueCheck == false
                                                   select o).FirstOrDefault();
                if (findQueueWaitingId != null)
                {
                    int followId = findQueueWaitingId.Id;
                    await QueueRepository.ChangeWaitingAfterYou(followId);
                }
            }

            /*****************************************************/

            if (data.ResName == "BarBQPlaza")
            {
                // get id to change status standby --> accept
                var findId = (from o in QueueRepository.BarBQPlazaTableEntity
                              where o.ResBranch == data.ResBranch && o.UserId == data.UserId && data.ResName == data.ResName && o.QueueType == data.QueueType && o.Status == "standby"
                              select o).ToList();
                int Id = 0;
                foreach (BarBQPlazaTable d in findId)
                {
                    Id = d.Id;
                }
                await QueueRepository.QueueSkipBarBQPlaza(Id);

                // change follow queue waiting --> standby
                // caution should ignore cancel status

                BarBQPlazaTable findQueueWaitingId = (from o in QueueRepository.BarBQPlazaTableEntity
                                                    where o.ResBranch == data.ResBranch && o.QueueType == data.QueueType && o.QueueCheck == false
                                                    select o).FirstOrDefault();
                if (findQueueWaitingId != null)
                {
                    int followId = findQueueWaitingId.Id;
                    await QueueRepository.ChangeWaitingBarBQPlaza(followId);
                }
            }

            if (data.ResName == "EatAmAre")
            {
                // get id to change status standby --> accept
                var findId = (from o in QueueRepository.EatAmAreTableEntity
                              where o.ResBranch == data.ResBranch && o.UserId == data.UserId && data.ResName == data.ResName && o.QueueType == data.QueueType && o.Status == "standby"
                              select o).ToList();
                int Id = 0;
                foreach (EatAmAreTable d in findId)
                {
                    Id = d.Id;
                }
                await QueueRepository.QueueSkipEatAmAre(Id);

                // change follow queue waiting --> standby
                // caution should ignore cancel status

                EatAmAreTable findQueueWaitingId = (from o in QueueRepository.EatAmAreTableEntity
                                                      where o.ResBranch == data.ResBranch && o.QueueType == data.QueueType && o.QueueCheck == false
                                                      select o).FirstOrDefault();
                if (findQueueWaitingId != null)
                {
                    int followId = findQueueWaitingId.Id;
                    await QueueRepository.ChangeWaitingEatAmAre(followId);
                }
            }

            resp.Success = true;
            await UserRepository.ChangeIsReserveAsync(data.UserId);

            return resp;

        }



        /*********End Restaurant Call Service**********/



        /************User Call Service*****************/

        [HttpPost]
        [ActionName("Reserving")]
        public async Task<ReserveResponse> Reserving(ReserveData data)
        {
            ReserveResponse s = new ReserveResponse();

            

            // check token
            User getUser = (from o in UserRepository.Users
                              where o.Token == data.Token
                              select o).FirstOrDefault();
            if (getUser == null)
            {
                s.Error = true;
                s.MsgEng = "Failed to reserved please logout and login again";
                s.MsgThai = "เกิดข้อผิดพลาดกรุณาออกจากระบบและเชื่อมต่อเข้าบัญชีอีกครั้ง";
                return s;
            }
            // check user is reserving yet ?
            if (getUser.IsReserve)
            {
                s.Error = true;
                s.MsgEng = "You already reserved";
                s.MsgThai = "คุณได้ทำการจองไปแล้ว";
                return s;
            }
            // check panalty
            if (DateTime.Now < getUser.PanaltyTime)
            {
                DateTime endtime = (DateTime)getUser.PanaltyTime;
                TimeSpan span = endtime.Subtract(DateTime.Now);
                s.Error = true;
                s.MsgEng = "You canceling reservation 2 times successive,you can reserve in " + span.Minutes + " Minutes";
                s.MsgThai = "คุณได้ทำการยกเลิกติดต่อเกิน 2 ครั้งจะทำการจองได้ในเวลาอีก " + span.Minutes + " นาที";
                return s;
            }

            /******************************** BonChon *******************************/
            if(data.ResName == "BonChon")
            {
                var qCount = (from o in QueueRepository.BonChonTableEntity
                              where o.ResBranch == data.ResBranch && o.QueueType == data.QueueType && o.QueueCheck == false
                              select o).Count();
                var nextQCount = qCount + 1;

                

                
                if (data.CurrentUserQueue != qCount)
                {
                    s.Error = true;
                    s.MsgEng = "Current Queue Has Been Change, Currently is " + nextQCount + data.QueueType;
                    s.MsgThai = "คิวปัจจุบันของคุณมีการเปลี่ยนแปลง ปัจจุบันคือคิวที่ " + nextQCount + data.QueueType + "กรุณาทำการจองใหม่";
                    return s;
                } else 
                {
                    /*********** Not check panalty reserve yet ********/

                    BonChonTable reserved = new BonChonTable();
                    if (qCount == 0)
                    {
                        reserved.Status = "standby";
                        reserved.QueueNum = 1;
                    }
                    else
                    {
                        BonChonTable lastQ = (from o in QueueRepository.BonChonTableEntity
                                              where o.ResBranch == data.ResBranch && o.QueueType == data.QueueType && o.QueueCheck == false
                                              select o).OrderByDescending(o => o.QueueNum).FirstOrDefault();

                        int sendQueue = lastQ.QueueNum + 1;
                        reserved.QueueNum = sendQueue;
                        reserved.Status = "waiting";
                      
                        //var tmp = (from o in QueueRepository.BonChonTableEntity
                        //                 where o.ResBranch == data.ResBranch && o.QueueType == data.QueueType && o.QueueNum == qCount
                        //                 select o).ToList();
                        //foreach (BonChonTable a in tmp)
                        //{
                        //    if (a.QueueCheck == false)
                        //    {
                        //        reserved.Status = "waiting";
                        //    }
                        //    else
                        //    {
                        //        reserved.Status = "standby";
                        //    }
                        //}
                    }

                    // change IsReserved to true;
                    await UserRepository.ChangeIsReserveAsync(getUser.Id);

                    
                    reserved.Id = 0;
                    reserved.UserId = getUser.Id;
                    reserved.Nickname = data.Nickname;

                    reserved.ReserveTime = DateTime.Now;
                    reserved.ArriveTime = null;

                    reserved.QueueType = data.QueueType;
                    reserved.QueueCheck = false;
                    reserved.ResBranch = data.ResBranch;
                    reserved.ReserveLocation = "none";
                    await QueueRepository.AddQueueBonchon(reserved);

                    s.Error = false;
                    s.MsgEng = "Reserved";
                    s.MsgThai = "ทำการจองเรียบร้อย";

                    return s;
                }

            }

            /**************************************AfterYou*******************************************/
            if(data.ResName == "AfterYou")
            {
                var qCount = (from o in QueueRepository.AfterYouTableEntity
                              where o.ResBranch == data.ResBranch && o.QueueType == data.QueueType && o.QueueCheck == false
                              select o).Count();
                var nextQCount = qCount + 1;


                if (data.CurrentUserQueue != qCount)
                {
                    s.Error = true;
                    s.MsgEng = "Current Queue Has Been Change, Currently is " + nextQCount + data.QueueType;
                    s.MsgThai = "คิวปัจจุบันของคุณมีการเปลี่ยนแปลง ปัจจุบันคือคิวที่ " + nextQCount + data.QueueType;
                    return s;
                }
                else
                {
                    /*********** Not check panalty reserve yet ********/

                    AfterYouTable reserved = new AfterYouTable();
                    if (qCount == 0)
                    {
                        reserved.Status = "standby";
                        reserved.QueueNum = 1;
                    }
                    else
                    {

                        AfterYouTable lastQ = (from o in QueueRepository.AfterYouTableEntity
                                              where o.ResBranch == data.ResBranch && o.QueueType == data.QueueType && o.QueueCheck == false
                                              select o).OrderByDescending(o => o.QueueNum).FirstOrDefault();

                        int sendQueue = lastQ.QueueNum + 1;
                        reserved.QueueNum = sendQueue;
                        reserved.Status = "waiting";
                    }

                    // change IsReserved to true;
                    await UserRepository.ChangeIsReserveAsync(getUser.Id);


                    reserved.Id = 0;
                    reserved.UserId = getUser.Id;
                    reserved.Nickname = data.Nickname;

                    reserved.ReserveTime = DateTime.Now;
                    reserved.ArriveTime = null;

                    reserved.QueueType = data.QueueType;
                    reserved.QueueCheck = false;
                    reserved.ResBranch = data.ResBranch;
                    reserved.ReserveLocation = "none";
                    await QueueRepository.AddQueueAfterYou(reserved);

                    s.Error = false;
                    s.MsgEng = "Reserved";
                    s.MsgThai = "ทำการจองเรียบร้อย";

                    return s;
                }
            }

            /**************************************BarBQPlaza*******************************************/
            if (data.ResName == "BarBQPlaza")
            {
                var qCount = (from o in QueueRepository.BarBQPlazaTableEntity
                              where o.ResBranch == data.ResBranch && o.QueueType == data.QueueType && o.QueueCheck == false
                              select o).Count();
                var nextQCount = qCount + 1;


                if (data.CurrentUserQueue != qCount)
                {
                    s.Error = true;
                    s.MsgEng = "Current Queue Has Been Change, Currently is " + nextQCount + data.QueueType;
                    s.MsgThai = "คิวปัจจุบันของคุณมีการเปลี่ยนแปลง ปัจจุบันคือคิวที่ " + nextQCount + data.QueueType;
                    return s;
                }
                else
                {
                    /*********** Not check panalty reserve yet ********/

                    BarBQPlazaTable reserved = new BarBQPlazaTable();
                    if (qCount == 0)
                    {
                        reserved.Status = "standby";
                        reserved.QueueNum = 1;
                    }
                    else
                    {

                        BarBQPlazaTable lastQ = (from o in QueueRepository.BarBQPlazaTableEntity
                                               where o.ResBranch == data.ResBranch && o.QueueType == data.QueueType && o.QueueCheck == false
                                               select o).OrderByDescending(o => o.QueueNum).FirstOrDefault();

                        int sendQueue = lastQ.QueueNum + 1;
                        reserved.QueueNum = sendQueue;
                        reserved.Status = "waiting";
                    }

                    // change IsReserved to true;
                    await UserRepository.ChangeIsReserveAsync(getUser.Id);


                    reserved.Id = 0;
                    reserved.UserId = getUser.Id;
                    reserved.Nickname = data.Nickname;

                    reserved.ReserveTime = DateTime.Now;
                    reserved.ArriveTime = null;

                    reserved.QueueType = data.QueueType;
                    reserved.QueueCheck = false;
                    reserved.ResBranch = data.ResBranch;
                    reserved.ReserveLocation = "none";
                    await QueueRepository.AddQueueBarBQPlaza(reserved);

                    s.Error = false;
                    s.MsgEng = "Reserved";
                    s.MsgThai = "ทำการจองเรียบร้อย";

                    return s;
                }
            }

            /**************************************EatAmAre*******************************************/
            if (data.ResName == "EatAmAre")
            {
                var qCount = (from o in QueueRepository.EatAmAreTableEntity
                              where o.ResBranch == data.ResBranch && o.QueueType == data.QueueType && o.QueueCheck == false
                              select o).Count();
                var nextQCount = qCount + 1;


                if (data.CurrentUserQueue != qCount)
                {
                    s.Error = true;
                    s.MsgEng = "Current Queue Has Been Change, Currently is " + nextQCount + data.QueueType;
                    s.MsgThai = "คิวปัจจุบันของคุณมีการเปลี่ยนแปลง ปัจจุบันคือคิวที่ " + nextQCount + data.QueueType;
                    return s;
                }
                else
                {
                    /*********** Not check panalty reserve yet ********/

                    EatAmAreTable reserved = new EatAmAreTable();
                    if (qCount == 0)
                    {
                        reserved.Status = "standby";
                        reserved.QueueNum = 1;
                    }
                    else
                    {

                        EatAmAreTable lastQ = (from o in QueueRepository.EatAmAreTableEntity
                                                 where o.ResBranch == data.ResBranch && o.QueueType == data.QueueType && o.QueueCheck == false
                                                 select o).OrderByDescending(o => o.QueueNum).FirstOrDefault();

                        int sendQueue = lastQ.QueueNum + 1;
                        reserved.QueueNum = sendQueue;
                        reserved.Status = "waiting";
                    }

                    // change IsReserved to true;
                    await UserRepository.ChangeIsReserveAsync(getUser.Id);


                    reserved.Id = 0;
                    reserved.UserId = getUser.Id;
                    reserved.Nickname = data.Nickname;

                    reserved.ReserveTime = DateTime.Now;
                    reserved.ArriveTime = null;

                    reserved.QueueType = data.QueueType;
                    reserved.QueueCheck = false;
                    reserved.ResBranch = data.ResBranch;
                    reserved.ReserveLocation = "none";
                    await QueueRepository.AddQueueEatAmAre(reserved);

                    s.Error = false;
                    s.MsgEng = "Reserved";
                    s.MsgThai = "ทำการจองเรียบร้อย";

                    return s;
                }
            }
                

            return  s;
        }

        //IEnumerable<ResPickingResponse>
        [HttpPost]
        [ActionName("ResPicking")]
        public IEnumerable<ResPickingResponse> ResPicking()
        {
            List<ResPickingResponse> dataL = new List<ResPickingResponse>();
            
            var queryS = (from q in QueueRepository.ResStatusEntity
               select q.ResName).Distinct().ToList();

            foreach (var a in queryS)
            {
                ResStatus nameTemp = new ResStatus();

                ResPickingResponse r = new ResPickingResponse();
                ResStatus s = (from p in QueueRepository.ResStatusEntity
                               where p.ResName == a
                               select p).FirstOrDefault();

                r.ResName = a;
                r.ResType = s.ResType;
                r.CountBranch = (from o in QueueRepository.ResStatusEntity
                                 where o.ResName == a
                                 select o).Count();

                dataL.Add(r);

            }


            return dataL;
        }

        [HttpPost]
        [ActionName("ResBranchPicking")]
        public IEnumerable<ResBranchPickingResponse> ResBranchPicking([FromBody]ResBranchPickingData data)
        {
            
            List<ResBranchPickingResponse> dataL = new List<ResBranchPickingResponse>();
            var resBranchData = (from o in QueueRepository.ResStatusEntity
                                 where o.ResName == data.ResName
                                 select o).ToList();

            foreach (ResStatus a in resBranchData)
            {
                ResBranchPickingResponse response = new ResBranchPickingResponse();
                response.ResBranch = a.ResBranch;
                response.Region = a.Region;
                response.QueueStatus = a.QueueStatus;
                dataL.Add(response);
            }
            return dataL;
        }

        [HttpPost]
        [ActionName("CurrentQueueDetail")]
        public CurrentQueueDetailResponse CurrentQueueDetail(CurrentQueueDetailData data)
        {
            CurrentQueueDetailResponse resp = new CurrentQueueDetailResponse();

            if (data.ResName == "BonChon")
            {
                var totalQ = (from o in QueueRepository.BonChonTableEntity
                              where o.ResBranch == data.ResBranch && o.QueueType == data.QueueType && o.QueueCheck == false
                              select o).Count();

                resp.TotalQueue = totalQ;

                var standbyQ = (from o in QueueRepository.BonChonTableEntity
                                where o.ResBranch == data.ResBranch && o.QueueType == data.QueueType && o.QueueCheck == false && o.Status == "standby"
                                select o).ToList();
                foreach (BonChonTable s in standbyQ)
                {
                    resp.QueueNum = s.QueueNum;
                }

                resp.QueueType = data.QueueType;


            }
            
            /*************************************************/

            if (data.ResName == "AfterYou")
            {
                var totalQ = (from o in QueueRepository.AfterYouTableEntity
                              where o.ResBranch == data.ResBranch && o.QueueType == data.QueueType && o.QueueCheck == false
                              select o).Count();

                resp.TotalQueue = totalQ;

                var standbyQ = (from o in QueueRepository.AfterYouTableEntity
                                where o.ResBranch == data.ResBranch && o.QueueType == data.QueueType && o.QueueCheck == false && o.Status == "standby"
                                select o).ToList();
                foreach (AfterYouTable s in standbyQ)
                {
                    resp.QueueNum = s.QueueNum;
                }

                resp.QueueType = data.QueueType;
            }

            /*************************************************/

            if (data.ResName == "BarBQPlaza")
            {
                var totalQ = (from o in QueueRepository.BarBQPlazaTableEntity
                              where o.ResBranch == data.ResBranch && o.QueueType == data.QueueType && o.QueueCheck == false
                              select o).Count();

                resp.TotalQueue = totalQ;

                var standbyQ = (from o in QueueRepository.BarBQPlazaTableEntity
                                where o.ResBranch == data.ResBranch && o.QueueType == data.QueueType && o.QueueCheck == false && o.Status == "standby"
                                select o).ToList();
                foreach (BarBQPlazaTable s in standbyQ)
                {
                    resp.QueueNum = s.QueueNum;
                }

                resp.QueueType = data.QueueType;
            }

            /*************************************************/

            if (data.ResName == "EatAmAre")
            {
                var totalQ = (from o in QueueRepository.EatAmAreTableEntity
                              where o.ResBranch == data.ResBranch && o.QueueType == data.QueueType && o.QueueCheck == false
                              select o).Count();

                resp.TotalQueue = totalQ;

                var standbyQ = (from o in QueueRepository.EatAmAreTableEntity
                                where o.ResBranch == data.ResBranch && o.QueueType == data.QueueType && o.QueueCheck == false && o.Status == "standby"
                                select o).ToList();
                foreach (EatAmAreTable s in standbyQ)
                {
                    resp.QueueNum = s.QueueNum;
                }

                resp.QueueType = data.QueueType;
            }


            return resp;

        }


        [HttpPost]
        [ActionName("CancelReserve")]
        public async Task<CancelReserveResponse> CancelReserve(CancelReserveData data)
        {
            // find id to change QueueCheck --> true , status --> cancel

            CancelReserveResponse resp = new CancelReserveResponse();

            //casual token check
            User getUser = (from o in UserRepository.Users
                            where o.Token == data.Token
                            select o).FirstOrDefault();
            if (getUser == null)
            {
                resp.Success = false;
                return resp;
            }
            //reserved point
            if (!(getUser.ReservePoint == 0))
            {
                int reservepoint = getUser.ReservePoint - 1;
                await UserRepository.UpdateReservePoint(getUser.Id, reservepoint);
            }
            //set panalty
            if (getUser.ReservePoint <= 1)
            {
                await UserRepository.SetPanaltyTime(getUser.Id);
            }


            /*******************************************BonChon*******************************************************/
            if (data.ResName == "BonChon")
            {
                var findId = (from o in QueueRepository.BonChonTableEntity
                              where o.ResBranch == data.ResBranch && o.UserId == getUser.Id && data.ResName == data.ResName && o.QueueType == data.QueueType
                              select o).ToList();
                int Id = 0;
                bool isStandby = false;
                foreach (BonChonTable d in findId)
                {
                    Id = d.Id;
                    if (d.Status == "standby")
                    {
                        isStandby = true;
                    }
                }

                await QueueRepository.CancelQueueBonChon(Id);

                // when cancel queue is standby
                if (isStandby)
                {
                    BonChonTable findQueueWaitingId = (from o in QueueRepository.BonChonTableEntity
                                                       where o.ResBranch == data.ResBranch && o.QueueType == data.QueueType && o.QueueCheck == false
                                                       select o).FirstOrDefault();
                    if (findQueueWaitingId != null)
                    {
                        int followId = findQueueWaitingId.Id;
                        await QueueRepository.ChangeWaitingBonChon(followId);
                    }
                }
            }

            /*****************************************************************/

            if (data.ResName == "AfterYou")
            {
                var findId = (from o in QueueRepository.AfterYouTableEntity
                              where o.ResBranch == data.ResBranch && o.UserId == getUser.Id && data.ResName == data.ResName && o.QueueType == data.QueueType
                              select o).ToList();
                int Id = 0;
                bool isStandby = false;
                foreach (AfterYouTable d in findId)
                {
                    Id = d.Id;
                    if (d.Status == "standby")
                    {
                        isStandby = true;
                    }
                }

                await QueueRepository.CancelQueueAfterYou(Id);

                // when cancel queue is standby
                if (isStandby)
                {
                    AfterYouTable findQueueWaitingId = (from o in QueueRepository.AfterYouTableEntity
                                                       where o.ResBranch == data.ResBranch && o.QueueType == data.QueueType && o.QueueCheck == false
                                                       select o).FirstOrDefault();
                    if (findQueueWaitingId != null)
                    {
                        int followId = findQueueWaitingId.Id;
                        await QueueRepository.ChangeWaitingAfterYou(followId);
                    }
                }
            }

            /*****************************************************************/

            if (data.ResName == "BarBQPlaza")
            {
                var findId = (from o in QueueRepository.BarBQPlazaTableEntity
                              where o.ResBranch == data.ResBranch && o.UserId == getUser.Id && data.ResName == data.ResName && o.QueueType == data.QueueType
                              select o).ToList();
                int Id = 0;
                bool isStandby = false;
                foreach (BarBQPlazaTable d in findId)
                {
                    Id = d.Id;
                    if (d.Status == "standby")
                    {
                        isStandby = true;
                    }
                }

                await QueueRepository.CancelQueueBarBQPlaza(Id);

                // when cancel queue is standby
                if (isStandby)
                {
                    BarBQPlazaTable findQueueWaitingId = (from o in QueueRepository.BarBQPlazaTableEntity
                                                       where o.ResBranch == data.ResBranch && o.QueueType == data.QueueType && o.QueueCheck == false
                                                       select o).FirstOrDefault();
                    if (findQueueWaitingId != null)
                    {
                        int followId = findQueueWaitingId.Id;
                        await QueueRepository.ChangeWaitingBarBQPlaza(followId);
                    }
                }
            }

            /*****************************************************************/

            if (data.ResName == "EatAmAre")
            {
                var findId = (from o in QueueRepository.EatAmAreTableEntity
                              where o.ResBranch == data.ResBranch && o.UserId == getUser.Id && data.ResName == data.ResName && o.QueueType == data.QueueType
                              select o).ToList();
                int Id = 0;
                bool isStandby = false;
                foreach (EatAmAreTable d in findId)
                {
                    Id = d.Id;
                    if (d.Status == "standby")
                    {
                        isStandby = true;
                    }
                }

                await QueueRepository.CancelQueueEatAmAre(Id);

                // when cancel queue is standby
                if (isStandby)
                {
                    EatAmAreTable findQueueWaitingId = (from o in QueueRepository.EatAmAreTableEntity
                                                          where o.ResBranch == data.ResBranch && o.QueueType == data.QueueType && o.QueueCheck == false
                                                          select o).FirstOrDefault();
                    if (findQueueWaitingId != null)
                    {
                        int followId = findQueueWaitingId.Id;
                        await QueueRepository.ChangeWaitingEatAmAre(followId);
                    }
                }
            }

            await UserRepository.ChangeIsReserveAsync(getUser.Id);
            resp.Success = true;

            return resp;
        }

        [HttpPost]
        [ActionName("ReserveUpdate")]
        public ReserveUpdateResponse ReserveUpdate(ReserveUpdateData data)
        {

            ReserveUpdateResponse resp = new ReserveUpdateResponse();


            if (data.ResName == "BonChon")
            {

                var standbyQ = (from o in QueueRepository.BonChonTableEntity
                                where o.ResBranch == data.ResBranch && o.QueueType == data.QueueType && o.QueueCheck == false && o.Status == "standby"
                                select o).ToList();
                if (standbyQ == null) 
                {
                    resp.Error = true;
                    return resp;

                }
                else
                {
                    foreach (BonChonTable s in standbyQ)
                    {
                        resp.CurrentQueue = s.QueueNum;
                    }
                }
                

                resp.QueueType = data.QueueType;

                User getUser = (from o in UserRepository.Users
                                where o.Token == data.Token
                                select o).FirstOrDefault();

                BonChonTable userQ = (from o in QueueRepository.BonChonTableEntity
                                      where o.ResBranch == data.ResBranch && o.QueueType == data.QueueType && o.UserId == getUser.Id && o.QueueCheck == false
                                      select o).FirstOrDefault();



                if (userQ != null)
                {
                    // your queue > current queue false end activity
                    resp.YourQueue = userQ.QueueNum;
                    resp.Error = false;
                }
                else
                {
                    resp.Error = true;
                }

                return resp;

            }

            //////////////////////////////////////////////////////

            if (data.ResName == "AfterYou")
            {
                var standbyQ = (from o in QueueRepository.AfterYouTableEntity
                                where o.ResBranch == data.ResBranch && o.QueueType == data.QueueType && o.QueueCheck == false && o.Status == "standby"
                                select o).ToList();
                if (standbyQ == null)
                {
                    resp.Error = true;
                    return resp;

                }
                else
                {
                    foreach (AfterYouTable s in standbyQ)
                    {
                        resp.CurrentQueue = s.QueueNum;
                    }
                }


                resp.QueueType = data.QueueType;

                User getUser = (from o in UserRepository.Users
                                where o.Token == data.Token
                                select o).FirstOrDefault();

                AfterYouTable userQ = (from o in QueueRepository.AfterYouTableEntity
                                      where o.ResBranch == data.ResBranch && o.QueueType == data.QueueType && o.UserId == getUser.Id && o.QueueCheck == false
                                      select o).FirstOrDefault();



                if (userQ != null)
                {
                    // your queue > current queue false end activity
                    resp.YourQueue = userQ.QueueNum;
                    resp.Error = false;
                }
                else
                {
                    resp.Error = true;
                }

                return resp;
            }

            //////////////////////////////////////////////////////

            if (data.ResName == "BarBQPlaza")
            {
                var standbyQ = (from o in QueueRepository.BarBQPlazaTableEntity
                                where o.ResBranch == data.ResBranch && o.QueueType == data.QueueType && o.QueueCheck == false && o.Status == "standby"
                                select o).ToList();
                if (standbyQ == null)
                {
                    resp.Error = true;
                    return resp;

                }
                else
                {
                    foreach (BarBQPlazaTable s in standbyQ)
                    {
                        resp.CurrentQueue = s.QueueNum;
                    }
                }


                resp.QueueType = data.QueueType;

                User getUser = (from o in UserRepository.Users
                                where o.Token == data.Token
                                select o).FirstOrDefault();

                BarBQPlazaTable userQ = (from o in QueueRepository.BarBQPlazaTableEntity
                                         where o.ResBranch == data.ResBranch && o.QueueType == data.QueueType && o.UserId == getUser.Id && o.QueueCheck == false
                                      select o).FirstOrDefault();

                


                if (userQ != null)
                {
                    
                    resp.Error = false;
                    resp.YourQueue = userQ.QueueNum;
                }
                else
                {
                    // your queue > current queue false end activity
                    resp.Error = true;
                }

                return resp;
            }

            //////////////////////////////////////////////////////

            if (data.ResName == "EatAmAre")
            {
                var standbyQ = (from o in QueueRepository.EatAmAreTableEntity
                                where o.ResBranch == data.ResBranch && o.QueueType == data.QueueType && o.QueueCheck == false && o.Status == "standby"
                                select o).ToList();
                if (standbyQ == null)
                {
                    resp.Error = true;
                    return resp;

                }
                else
                {
                    foreach (EatAmAreTable s in standbyQ)
                    {
                        resp.CurrentQueue = s.QueueNum;
                    }
                }


                resp.QueueType = data.QueueType;

                User getUser = (from o in UserRepository.Users
                                where o.Token == data.Token
                                select o).FirstOrDefault();

                EatAmAreTable userQ = (from o in QueueRepository.EatAmAreTableEntity
                                         where o.ResBranch == data.ResBranch && o.QueueType == data.QueueType && o.UserId == getUser.Id && o.QueueCheck == false
                                         select o).FirstOrDefault();




                if (userQ != null)
                {

                    resp.Error = false;
                    resp.YourQueue = userQ.QueueNum;
                }
                else
                {
                    // your queue > current queue false end activity
                    resp.Error = true;
                }

                return resp;
            }


            return resp;
        }

        /**********End User Call Service **************/

    }
}