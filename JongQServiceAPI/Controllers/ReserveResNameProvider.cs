using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JongQServiceAPI.Models;

namespace JongQServiceAPI.Controllers
{
    public class ReserveResNameProvider
    {
        private QueueRepository QueueRepository;
        private IEnumerable<BonChonTable> tableBonChon;
        private IEnumerable<AfterYouTable> tableAfterYou;
        
        public ReserveResNameProvider()
        {
            //QueueRepository = new QueueRepository();
            //tableBonChon = QueueRepository.BonChonTableEntity();

        }
    }
}