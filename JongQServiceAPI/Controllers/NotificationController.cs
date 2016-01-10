using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.ServiceBus.Notifications;
using JongQServiceAPI.CustomResponseContent;
using System.Web.Http.ModelBinding;
using JongQServiceAPI.Models;
using System.Threading.Tasks;

namespace JongQServiceAPI.Controllers
{
    public class NotificationController : ApiController
    {
        //please replace the connectionString and notificationHubName fields below with your own notification hub connection info
        private const string _connectionString = "Endpoint=sb://pushnotijongq.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=WEJvo6zM3ImKvUsxEVOcoBz0pKSam/rZJnUbERGEEFg=";
        private const string _notificationHubName = "pushnotijongq";
        private readonly NotificationHubClient _hubClient;
        private QueueRepository QueueRepository { get; set; }
        private UserRepository UserRepository { get; set; }

        public NotificationController()
        {
            _hubClient = NotificationHubClient.CreateClientFromConnectionString(_connectionString, _notificationHubName);
            QueueRepository = new QueueRepository();
            UserRepository = new UserRepository();
        }

        [HttpPost]
        [ActionName("SaveRegisNoti")]
        public async Task<SaveRegisNotiResponse> SaveRegisNoti(SaveRegisNotiData data)
        {
            SaveRegisNotiResponse resp = new SaveRegisNotiResponse();
            User getUser = (from o in UserRepository.Users
                            where o.Token == data.Token
                            select o).FirstOrDefault();
            if (getUser != null)
            {
                await UserRepository.SaveRegisPushNotiIdAsync(getUser.Id, data.NotiKey);
                resp.Status = true;
            }
            else
            {
                resp.Status = false;
            }
            

            return resp;
        }
        [HttpPost]
        [ActionName("TestPush")]
        public TestPushResponse test(TestPushData data)
        {
            TestPushResponse a = new TestPushResponse();
            a.hello = data.hello;

            return a;
        }


        [HttpPost]
        [ActionName("PushNotificationRest")]
        public async Task<PushNotificationRestResponse> PushNotificationRest(PushNotificationRestData data)
        {
            PushNotificationRestResponse resp = new PushNotificationRestResponse();

            //TestPushResponse test = new TestPushResponse();

            /********************************** BonChon ********************************/
            if (data.ResName == "BonChon")
            {

                string BranchTag = data.ResName + "-" + data.ResBranch + "-" + data.QueueType;

                var getQueue = (from o in QueueRepository.BonChonTableEntity
                                where o.ResBranch == data.ResBranch && o.QueueType == data.QueueType && o.QueueCheck == false
                                select o).OrderBy(o => o.QueueNum).ToList();

                int countQueue = (from o in QueueRepository.BonChonTableEntity
                                  where o.ResBranch == data.ResBranch && o.QueueType == data.QueueType && o.QueueCheck == false
                                  select o).Count();

                //no queue in table
                if (getQueue == null)
                {
                    resp.status = false;
                    return resp;
                }


                // NOTI  SEARCH_ID --> REGISTER TO HUB --> PUSH NOTI TO REGISTER THAT REGISTED --> DELETE FROM HUB
                int countQ = 0;
                foreach (BonChonTable tmpQ in getQueue)
                {
                    if (!(tmpQ.UserId == 0))
                    {
                        User getUser = (from o in UserRepository.Users
                                        where o.Id == tmpQ.UserId
                                        select o).FirstOrDefault();
                        string getNotiKey = getUser.KeyNoti;

                        if (countQ == 0)
                        {
                            //tag
                            TagSend tag = new TagSend();
                            tag.HubTag = BranchTag + "-" + 0;
                            //regis
                            var hubRegistration = await RegisterDeviceWithNotificationHub(getNotiKey, tag);
                            var regisId = hubRegistration.RegistrationId;
                            //update tag
                            hubRegistration.Tags = new HashSet<string>() { tag.HubTag };
                            await _hubClient.UpdateRegistrationAsync(hubRegistration);
                            resp.msg = regisId;
                            //push noti
                            string notificationJsonPayload = "{\"data\" : " +
                                                                    "   {" +
                                                                        "   \"message\": \"" + "ถึงคิวของคุณแล้ว" + "\"" +
                                                                    "   }" +
                                                              "}";
                            var result = await _hubClient.SendGcmNativeNotificationAsync(notificationJsonPayload);
                            //delete
                            var getId = await _hubClient.GetRegistrationAsync<RegistrationDescription>(regisId);
                            await _hubClient.DeleteRegistrationAsync(getId);

                        }

                        if (countQ == 1)
                        {
                            //tag
                            TagSend tag = new TagSend();
                            tag.HubTag = BranchTag + "-" + 1;
                            //regis
                            var hubRegistration = await RegisterDeviceWithNotificationHub(getNotiKey, tag);
                            var regisId = hubRegistration.RegistrationId;
                            //update tag
                            hubRegistration.Tags = new HashSet<string>() { tag.HubTag };
                            await _hubClient.UpdateRegistrationAsync(hubRegistration);
                            resp.msg = regisId;
                            //push noti
                            string notificationJsonPayload = "{\"data\" : " +
                                                                    "   {" +
                                                                        "   \"message\": \"" + "อีก 1 คิวจะถึงคิวของคุณ" + "\"" +
                                                                    "   }" +
                                                              "}";
                            var result = await _hubClient.SendGcmNativeNotificationAsync(notificationJsonPayload);
                            //delete
                            var getId = await _hubClient.GetRegistrationAsync<RegistrationDescription>(regisId);
                            await _hubClient.DeleteRegistrationAsync(getId);
                        }

                        if (countQ == 3)
                        {
                            //tag
                            TagSend tag = new TagSend();
                            tag.HubTag = BranchTag + "-" + 3;
                            //regis
                            var hubRegistration = await RegisterDeviceWithNotificationHub(getNotiKey, tag);
                            var regisId = hubRegistration.RegistrationId;
                            //update tag
                            hubRegistration.Tags = new HashSet<string>() { tag.HubTag };
                            await _hubClient.UpdateRegistrationAsync(hubRegistration);
                            resp.msg = regisId;
                            //push noti
                            string notificationJsonPayload = "{\"data\" : " +
                                                                    "   {" +
                                                                        "   \"message\": \"" + "อีก 3 คิวจะถึงคิวของคุณ" + "\"" +
                                                                    "   }" +
                                                              "}";
                            var result = await _hubClient.SendGcmNativeNotificationAsync(notificationJsonPayload);
                            //delete
                            var getId = await _hubClient.GetRegistrationAsync<RegistrationDescription>(regisId);
                            await _hubClient.DeleteRegistrationAsync(getId);
                        }

                        if (countQ == 5)
                        {
                            //tag
                            TagSend tag = new TagSend();
                            tag.HubTag = BranchTag + "-" + 5;
                            //regis
                            var hubRegistration = await RegisterDeviceWithNotificationHub(getNotiKey, tag);
                            var regisId = hubRegistration.RegistrationId;
                            //update tag
                            hubRegistration.Tags = new HashSet<string>() { tag.HubTag };
                            await _hubClient.UpdateRegistrationAsync(hubRegistration);
                            resp.msg = regisId;
                            //push noti
                            string notificationJsonPayload = "{\"data\" : " +
                                                                    "   {" +
                                                                        "   \"message\": \"" + "อีก 5 คิวจะถึงคิวของคุณ" + "\"" +
                                                                    "   }" +
                                                              "}";
                            var result = await _hubClient.SendGcmNativeNotificationAsync(notificationJsonPayload);
                            //delete
                            var getId = await _hubClient.GetRegistrationAsync<RegistrationDescription>(regisId);
                            await _hubClient.DeleteRegistrationAsync(getId);
                        }


                    }

                    if (countQ > 5)
                    {
                        //nomore device to regis to hub
                        break;
                    }

                    countQ++;

                }

            }

            /********************************** AfterYou ********************************/
            if (data.ResName == "AfterYou")
            {

                //test.hello = data.ResName;
                //return test;
                string BranchTag = data.ResName + "-" + data.ResBranch + "-" + data.QueueType;

                var getQueue = (from o in QueueRepository.AfterYouTableEntity
                                where o.ResBranch == data.ResBranch && o.QueueType == data.QueueType && o.QueueCheck == false
                                select o).OrderBy(o => o.QueueNum).ToList();

                int countQueue = (from o in QueueRepository.AfterYouTableEntity
                                  where o.ResBranch == data.ResBranch && o.QueueType == data.QueueType && o.QueueCheck == false
                                  select o).Count();

                //no queue in table
                if (getQueue == null)
                {
                    resp.status = true;
                    return resp;
                }


                // NOTI  SEARCH_ID --> REGISTER TO HUB --> PUSH NOTI TO REGISTER THAT REGISTED --> DELETE FROM HUB
                int countQ = 0;
                foreach (AfterYouTable tmpQ in getQueue)
                {
                    if (!(tmpQ.UserId == 0))
                    {
                        User getUser = (from o in UserRepository.Users
                                        where o.Id == tmpQ.UserId
                                        select o).FirstOrDefault();
                        string getNotiKey = getUser.KeyNoti;

                        if (countQ == 0)
                        {
                            //tag
                            TagSend tag = new TagSend();
                            tag.HubTag = BranchTag + "-" + 0;
                            //regis
                            var hubRegistration = await RegisterDeviceWithNotificationHub(getNotiKey, tag);
                            var regisId = hubRegistration.RegistrationId;
                            //update tag
                            hubRegistration.Tags = new HashSet<string>() { tag.HubTag };
                            await _hubClient.UpdateRegistrationAsync(hubRegistration);
                            resp.msg = regisId;
                            //push noti
                            string notificationJsonPayload = "{\"data\" : " +
                                                                    "   {" +
                                                                        "   \"message\": \"" + "ถึงคิวของคุณแล้ว" + "\"" +
                                                                    "   }" +
                                                              "}";
                            var result = await _hubClient.SendGcmNativeNotificationAsync(notificationJsonPayload);
                            //delete
                            var getId = await _hubClient.GetRegistrationAsync<RegistrationDescription>(regisId);
                            await _hubClient.DeleteRegistrationAsync(getId);

                        }

                        if (countQ == 1)
                        {
                            //tag
                            TagSend tag = new TagSend();
                            tag.HubTag = BranchTag + "-" + 1;
                            //regis
                            var hubRegistration = await RegisterDeviceWithNotificationHub(getNotiKey, tag);
                            var regisId = hubRegistration.RegistrationId;
                            //update tag
                            hubRegistration.Tags = new HashSet<string>() { tag.HubTag };
                            await _hubClient.UpdateRegistrationAsync(hubRegistration);
                            resp.msg = regisId;
                            //push noti
                            string notificationJsonPayload = "{\"data\" : " +
                                                                    "   {" +
                                                                        "   \"message\": \"" + "อีก 1 คิวจะถึงคิวของคุณ" + "\"" +
                                                                    "   }" +
                                                              "}";
                            var result = await _hubClient.SendGcmNativeNotificationAsync(notificationJsonPayload);
                            //delete
                            var getId = await _hubClient.GetRegistrationAsync<RegistrationDescription>(regisId);
                            await _hubClient.DeleteRegistrationAsync(getId);
                        }

                        if (countQ == 3)
                        {
                            //tag
                            TagSend tag = new TagSend();
                            tag.HubTag = BranchTag + "-" + 3;
                            //regis
                            var hubRegistration = await RegisterDeviceWithNotificationHub(getNotiKey, tag);
                            var regisId = hubRegistration.RegistrationId;
                            //update tag
                            hubRegistration.Tags = new HashSet<string>() { tag.HubTag };
                            await _hubClient.UpdateRegistrationAsync(hubRegistration);
                            resp.msg = regisId;
                            //push noti
                            string notificationJsonPayload = "{\"data\" : " +
                                                                    "   {" +
                                                                        "   \"message\": \"" + "อีก 3 คิวจะถึงคิวของคุณ" + "\"" +
                                                                    "   }" +
                                                              "}";
                            var result = await _hubClient.SendGcmNativeNotificationAsync(notificationJsonPayload);
                            //delete
                            var getId = await _hubClient.GetRegistrationAsync<RegistrationDescription>(regisId);
                            await _hubClient.DeleteRegistrationAsync(getId);
                        }

                        if (countQ == 5)
                        {
                            //tag
                            TagSend tag = new TagSend();
                            tag.HubTag = BranchTag + "-" + 5;
                            //regis
                            var hubRegistration = await RegisterDeviceWithNotificationHub(getNotiKey, tag);
                            var regisId = hubRegistration.RegistrationId;
                            //update tag
                            hubRegistration.Tags = new HashSet<string>() { tag.HubTag };
                            await _hubClient.UpdateRegistrationAsync(hubRegistration);
                            resp.msg = regisId;
                            //push noti
                            string notificationJsonPayload = "{\"data\" : " +
                                                                    "   {" +
                                                                        "   \"message\": \"" + "อีก 5 คิวจะถึงคิวของคุณ" + "\"" +
                                                                    "   }" +
                                                              "}";
                            var result = await _hubClient.SendGcmNativeNotificationAsync(notificationJsonPayload);
                            //delete
                            var getId = await _hubClient.GetRegistrationAsync<RegistrationDescription>(regisId);
                            await _hubClient.DeleteRegistrationAsync(getId);
                        }

                        
                    }

                    if (countQ > 5)
                    {
                        //nomore device to regis to hub
                        break;
                    }

                    countQ++;

                }

            }

            /********************************** BarBQPlaza ********************************/
            if (data.ResName == "BarBQPlaza")
            {
                string BranchTag = data.ResName + "-" + data.ResBranch + "-" + data.QueueType;

                var getQueue = (from o in QueueRepository.BarBQPlazaTableEntity
                                where o.ResBranch == data.ResBranch && o.QueueType == data.QueueType && o.QueueCheck == false
                                select o).OrderBy(o => o.QueueNum).ToList();

                int countQueue = (from o in QueueRepository.BarBQPlazaTableEntity
                                  where o.ResBranch == data.ResBranch && o.QueueType == data.QueueType && o.QueueCheck == false
                                  select o).Count();

                //no queue in table
                if (getQueue == null)
                {
                    resp.status = true;
                    return resp;
                }


                // NOTI  SEARCH_ID --> REGISTER TO HUB --> PUSH NOTI TO REGISTER THAT REGISTED --> DELETE FROM HUB
                int countQ = 0;
                foreach (BarBQPlazaTable tmpQ in getQueue)
                {
                    if (!(tmpQ.UserId == 0))
                    {
                        User getUser = (from o in UserRepository.Users
                                        where o.Id == tmpQ.UserId
                                        select o).FirstOrDefault();
                        string getNotiKey = getUser.KeyNoti;

                        if (countQ == 0)
                        {
                            //tag
                            TagSend tag = new TagSend();
                            tag.HubTag = BranchTag + "-" + 0;
                            //regis
                            var hubRegistration = await RegisterDeviceWithNotificationHub(getNotiKey, tag);
                            var regisId = hubRegistration.RegistrationId;
                            //update tag
                            hubRegistration.Tags = new HashSet<string>() { tag.HubTag };
                            await _hubClient.UpdateRegistrationAsync(hubRegistration);
                            resp.msg = regisId;
                            //push noti
                            string notificationJsonPayload = "{\"data\" : " +
                                                                    "   {" +
                                                                        "   \"message\": \"" + "ถึงคิวของคุณแล้ว" + "\"" +
                                                                    "   }" +
                                                              "}";
                            var result = await _hubClient.SendGcmNativeNotificationAsync(notificationJsonPayload);
                            //delete
                            var getId = await _hubClient.GetRegistrationAsync<RegistrationDescription>(regisId);
                            await _hubClient.DeleteRegistrationAsync(getId);

                        }

                        if (countQ == 1)
                        {
                            //tag
                            TagSend tag = new TagSend();
                            tag.HubTag = BranchTag + "-" + 1;
                            //regis
                            var hubRegistration = await RegisterDeviceWithNotificationHub(getNotiKey, tag);
                            var regisId = hubRegistration.RegistrationId;
                            //update tag
                            hubRegistration.Tags = new HashSet<string>() { tag.HubTag };
                            await _hubClient.UpdateRegistrationAsync(hubRegistration);
                            resp.msg = regisId;
                            //push noti
                            string notificationJsonPayload = "{\"data\" : " +
                                                                    "   {" +
                                                                        "   \"message\": \"" + "อีก 1 คิวจะถึงคิวของคุณ" + "\"" +
                                                                    "   }" +
                                                              "}";
                            var result = await _hubClient.SendGcmNativeNotificationAsync(notificationJsonPayload);
                            //delete
                            var getId = await _hubClient.GetRegistrationAsync<RegistrationDescription>(regisId);
                            await _hubClient.DeleteRegistrationAsync(getId);
                        }

                        if (countQ == 3)
                        {
                            //tag
                            TagSend tag = new TagSend();
                            tag.HubTag = BranchTag + "-" + 3;
                            //regis
                            var hubRegistration = await RegisterDeviceWithNotificationHub(getNotiKey, tag);
                            var regisId = hubRegistration.RegistrationId;
                            //update tag
                            hubRegistration.Tags = new HashSet<string>() { tag.HubTag };
                            await _hubClient.UpdateRegistrationAsync(hubRegistration);
                            resp.msg = regisId;
                            //push noti
                            string notificationJsonPayload = "{\"data\" : " +
                                                                    "   {" +
                                                                        "   \"message\": \"" + "อีก 3 คิวจะถึงคิวของคุณ" + "\"" +
                                                                    "   }" +
                                                              "}";
                            var result = await _hubClient.SendGcmNativeNotificationAsync(notificationJsonPayload);
                            //delete
                            var getId = await _hubClient.GetRegistrationAsync<RegistrationDescription>(regisId);
                            await _hubClient.DeleteRegistrationAsync(getId);
                        }

                        if (countQ == 5)
                        {
                            //tag
                            TagSend tag = new TagSend();
                            tag.HubTag = BranchTag + "-" + 5;
                            //regis
                            var hubRegistration = await RegisterDeviceWithNotificationHub(getNotiKey, tag);
                            var regisId = hubRegistration.RegistrationId;
                            //update tag
                            hubRegistration.Tags = new HashSet<string>() { tag.HubTag };
                            await _hubClient.UpdateRegistrationAsync(hubRegistration);
                            resp.msg = regisId;
                            //push noti
                            string notificationJsonPayload = "{\"data\" : " +
                                                                    "   {" +
                                                                        "   \"message\": \"" + "อีก 5 คิวจะถึงคิวของคุณ" + "\"" +
                                                                    "   }" +
                                                              "}";
                            var result = await _hubClient.SendGcmNativeNotificationAsync(notificationJsonPayload);
                            //delete
                            var getId = await _hubClient.GetRegistrationAsync<RegistrationDescription>(regisId);
                            await _hubClient.DeleteRegistrationAsync(getId);
                        }


                    }

                    if (countQ > 5)
                    {
                        //nomore device to regis to hub
                        break;
                    }

                    countQ++;

                }

            }

            /********************************** EatAmAre ********************************/
            if (data.ResName == "EatAmAre")
            {
                string BranchTag = data.ResName + "-" + data.ResBranch + "-" + data.QueueType;

                var getQueue = (from o in QueueRepository.EatAmAreTableEntity
                                where o.ResBranch == data.ResBranch && o.QueueType == data.QueueType && o.QueueCheck == false
                                select o).OrderBy(o => o.QueueNum).ToList();

                int countQueue = (from o in QueueRepository.EatAmAreTableEntity
                                  where o.ResBranch == data.ResBranch && o.QueueType == data.QueueType && o.QueueCheck == false
                                  select o).Count();

                //no queue in table
                if (getQueue == null)
                {
                    resp.status = true;
                    return resp;
                }


                // NOTI  SEARCH_ID --> REGISTER TO HUB --> PUSH NOTI TO REGISTER THAT REGISTED --> DELETE FROM HUB
                int countQ = 0;
                foreach (EatAmAreTable tmpQ in getQueue)
                {
                    if (!(tmpQ.UserId == 0))
                    {
                        User getUser = (from o in UserRepository.Users
                                        where o.Id == tmpQ.UserId
                                        select o).FirstOrDefault();
                        string getNotiKey = getUser.KeyNoti;

                        if (countQ == 0)
                        {
                            //tag
                            TagSend tag = new TagSend();
                            tag.HubTag = BranchTag + "-" + 0;
                            //regis
                            var hubRegistration = await RegisterDeviceWithNotificationHub(getNotiKey, tag);
                            var regisId = hubRegistration.RegistrationId;
                            //update tag
                            hubRegistration.Tags = new HashSet<string>() { tag.HubTag };
                            await _hubClient.UpdateRegistrationAsync(hubRegistration);
                            resp.msg = regisId;
                            //push noti
                            string notificationJsonPayload = "{\"data\" : " +
                                                                    "   {" +
                                                                        "   \"message\": \"" + "ถึงคิวของคุณแล้ว" + "\"" +
                                                                    "   }" +
                                                              "}";
                            var result = await _hubClient.SendGcmNativeNotificationAsync(notificationJsonPayload);
                            //delete
                            var getId = await _hubClient.GetRegistrationAsync<RegistrationDescription>(regisId);
                            await _hubClient.DeleteRegistrationAsync(getId);

                        }

                        if (countQ == 1)
                        {
                            //tag
                            TagSend tag = new TagSend();
                            tag.HubTag = BranchTag + "-" + 1;
                            //regis
                            var hubRegistration = await RegisterDeviceWithNotificationHub(getNotiKey, tag);
                            var regisId = hubRegistration.RegistrationId;
                            //update tag
                            hubRegistration.Tags = new HashSet<string>() { tag.HubTag };
                            await _hubClient.UpdateRegistrationAsync(hubRegistration);
                            resp.msg = regisId;
                            //push noti
                            string notificationJsonPayload = "{\"data\" : " +
                                                                    "   {" +
                                                                        "   \"message\": \"" + "อีก 1 คิวจะถึงคิวของคุณ" + "\"" +
                                                                    "   }" +
                                                              "}";
                            var result = await _hubClient.SendGcmNativeNotificationAsync(notificationJsonPayload);
                            //delete
                            var getId = await _hubClient.GetRegistrationAsync<RegistrationDescription>(regisId);
                            await _hubClient.DeleteRegistrationAsync(getId);
                        }

                        if (countQ == 3)
                        {
                            //tag
                            TagSend tag = new TagSend();
                            tag.HubTag = BranchTag + "-" + 3;
                            //regis
                            var hubRegistration = await RegisterDeviceWithNotificationHub(getNotiKey, tag);
                            var regisId = hubRegistration.RegistrationId;
                            //update tag
                            hubRegistration.Tags = new HashSet<string>() { tag.HubTag };
                            await _hubClient.UpdateRegistrationAsync(hubRegistration);
                            resp.msg = regisId;
                            //push noti
                            string notificationJsonPayload = "{\"data\" : " +
                                                                    "   {" +
                                                                        "   \"message\": \"" + "อีก 3 คิวจะถึงคิวของคุณ" + "\"" +
                                                                    "   }" +
                                                              "}";
                            var result = await _hubClient.SendGcmNativeNotificationAsync(notificationJsonPayload);
                            //delete
                            var getId = await _hubClient.GetRegistrationAsync<RegistrationDescription>(regisId);
                            await _hubClient.DeleteRegistrationAsync(getId);
                        }

                        if (countQ == 5)
                        {
                            //tag
                            TagSend tag = new TagSend();
                            tag.HubTag = BranchTag + "-" + 5;
                            //regis
                            var hubRegistration = await RegisterDeviceWithNotificationHub(getNotiKey, tag);
                            var regisId = hubRegistration.RegistrationId;
                            //update tag
                            hubRegistration.Tags = new HashSet<string>() { tag.HubTag };
                            await _hubClient.UpdateRegistrationAsync(hubRegistration);
                            resp.msg = regisId;
                            //push noti
                            string notificationJsonPayload = "{\"data\" : " +
                                                                    "   {" +
                                                                        "   \"message\": \"" + "อีก 5 คิวจะถึงคิวของคุณ" + "\"" +
                                                                    "   }" +
                                                              "}";
                            var result = await _hubClient.SendGcmNativeNotificationAsync(notificationJsonPayload);
                            //delete
                            var getId = await _hubClient.GetRegistrationAsync<RegistrationDescription>(regisId);
                            await _hubClient.DeleteRegistrationAsync(getId);
                        }


                    }

                    if (countQ > 5)
                    {
                        //nomore device to regis to hub
                        break;
                    }

                    countQ++;

                }

            }


            resp.status = true;
            return resp;
        }

        /******************** RegistationToHub ***********************************/
        private async Task<RegistrationDescription> RegisterDeviceWithNotificationHub(string NotiKey,TagSend tag)
        {


            //var hubRegistrationId = device.HubRegistrationId ?? "0";//null or empty string as query input throws exception
            //var hubRegistration = await _hubClient.GetRegistrationAsync<RegistrationDescription>(hubRegistrationId);
            //if (hubRegistration != null)
            //{
            //    hubRegistration.Tags = hubTags;
            //    await _hubClient.UpdateRegistrationAsync(hubRegistration);
            //}
            //else
            //{
            //    hubRegistration = await _hubClient.CreateGcmNativeRegistrationAsync(NotiKey, Tag);
            //}

            var hubRegistration = await _hubClient.CreateGcmNativeRegistrationAsync(NotiKey);
            return hubRegistration;
        }

        private class TagSend
        {
            public string HubTag { get; set; }
        }




    }

}
