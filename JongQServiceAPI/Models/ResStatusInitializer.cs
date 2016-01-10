using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;

namespace JongQServiceAPI.Models
{
    public class ResStatusInitializer
        : DropCreateDatabaseIfModelChanges<QueueDbContext>
    {
        protected override void Seed(QueueDbContext context)
        {
            new List<ResStatus> {
            
                //new ResStatus(){ResName = "BonChon", ResBranch = "ทองหล่อ 13", QueueStatus = false, Region = "Central", ResType = "Grill"},
                //new ResStatus(){ResName = "BonChon", ResBranch = "เซ็นทรัลเวิล์ดชั้น 6", QueueStatus = false, Region = "Central", ResType = "Grill"},
                //new ResStatus(){ResName = "BonChon", ResBranch = "สีลมคอมเพล็กซ์ชั้นใต้ดิน", QueueStatus = false, Region = "Central", ResType = "Grill"},
                //new ResStatus(){ResName = "BonChon", ResBranch = "สยามเซ็นเตอร์ชั้น 4", QueueStatus = false, Region = "Central", ResType = "Grill"},
                //new ResStatus(){ResName = "BonChon", ResBranch = "the circle ราชพฤกษ์", QueueStatus = false, Region = "Central", ResType = "Grill"},
                //new ResStatus(){ResName = "BonChon", ResBranch = "Mercury Ville สถานี BTS ชิดลม", QueueStatus = false, Region = "Central", ResType = "Grill"},
                //new ResStatus(){ResName = "BonChon", ResBranch = "Terminal 21", QueueStatus = false, Region = "Central", ResType = "Grill"},
                //new ResStatus(){ResName = "BonChon", ResBranch = "อารีย์ villa", QueueStatus = false, Region = "Central", ResType = "Grill"},
                //new ResStatus(){ResName = "BarBQPlaza", ResBranch = "เซ็นทรัล ชลบุรี", QueueStatus = false, Region = "Central", ResType = "Grill"},
                //new ResStatus(){ResName = "BarBQPlaza", ResBranch = "เซ็นทรัล พัทยา", QueueStatus = false, Region = "Central", ResType = "Grill"},
                //new ResStatus(){ResName = "BarBQPlaza", ResBranch = "เซ็นทรัล พัทยาบีช", QueueStatus = false, Region = "Central", ResType = "Grill"},
                //new ResStatus(){ResName = "BarBQPlaza", ResBranch = "บิ๊กซี ชลบุรี", QueueStatus = false, Region = "Central", ResType = "Grill"},
                //new ResStatus(){ResName = "BarBQPlaza", ResBranch = "บิ๊กซี นครปฐม", QueueStatus = false, Region = "Central", ResType = "Grill"},
                //new ResStatus(){ResName = "BarBQPlaza", ResBranch = "บิ๊กซี ราชบุรี", QueueStatus = false, Region = "Central", ResType = "Grill"},
                //new ResStatus(){ResName = "BarBQPlaza", ResBranch = "บิ๊กซี อ้อมใหญ่", QueueStatus = false, Region = "Central", ResType = "Grill"},
                //new ResStatus(){ResName = "BarBQPlaza", ResBranch = "แปซิฟิก ศรีราชา", QueueStatus = false, Region = "Central", ResType = "Grill"},
                //new ResStatus(){ResName = "BarBQPlaza", ResBranch = "โลตัส ศาลายา", QueueStatus = false, Region = "Central", ResType = "Grill"},
                //new ResStatus(){ResName = "BarBQPlaza", ResBranch = "อยุธยา ปาร์ค", QueueStatus = false, Region = "Central", ResType = "Grill"},
                //new ResStatus(){ResName = "BarBQPlaza", ResBranch = "เชียงใหม่ แอร์พอร์ต", QueueStatus = false, Region = "North", ResType = "Grill"},
                //new ResStatus(){ResName = "BarBQPlaza", ResBranch = "เซ็นทรัลพลาซา เชียงราย", QueueStatus = false, Region = "North", ResType = "Grill"},
                //new ResStatus(){ResName = "BarBQPlaza", ResBranch = "เซ็นทรัล ขอนแก่น", QueueStatus = false, Region = "Northeast", ResType = "Grill"},
                //new ResStatus(){ResName = "BarBQPlaza", ResBranch = "โลตัส บ้านโป่ง", QueueStatus = false, Region = "Northeast", ResType = "Grill"},
                //new ResStatus(){ResName = "BarBQPlaza", ResBranch = "เซ็นทรัล เฟรสติวัล", QueueStatus = false, Region = "South", ResType = "Grill"},
                //new ResStatus(){ResName = "BarBQPlaza", ResBranch = "เดอะมอลล์ โคราช", QueueStatus = false, Region = "South", ResType = "Grill"},
                //new ResStatus(){ResName = "BarBQPlaza", ResBranch = "บิ๊กซี ฉะเชิงเทรา", QueueStatus = false, Region = "East", ResType = "Grill"},
                //new ResStatus(){ResName = "BarBQPlaza", ResBranch = "บิ๊กซี ระยอง", QueueStatus = false, Region = "East", ResType = "Grill"},
                //new ResStatus(){ResName = "BarBQPlaza", ResBranch = "แหลมทองระยอง", QueueStatus = false, Region = "East", ResType = "Grill"},
                //new ResStatus(){ResName = "AfterYou", ResBranch = "J avenue", QueueStatus = false, Region = "Central", ResType = "Grill"},
                //new ResStatus(){ResName = "AfterYou", ResBranch = "Lavilla, Paholyotin", QueueStatus = false, Region = "Central", ResType = "Grill"},
                //new ResStatus(){ResName = "AfterYou", ResBranch = "Siam Paragon", QueueStatus = false, Region = "Central", ResType = "Grill"},
                //new ResStatus(){ResName = "AfterYou", ResBranch = "The Crystal", QueueStatus = false, Region = "Central", ResType = "Grill"},
                //new ResStatus(){ResName = "AfterYou", ResBranch = "Central Plaza Ladprao", QueueStatus = false, Region = "Central", ResType = "Grill"},
                //new ResStatus(){ResName = "AfterYou", ResBranch = "Central Silom Complex", QueueStatus = false, Region = "Central", ResType = "Grill"},
                new ResStatus(){ResName = "EatAmAre", ResBranch = "Center One", QueueStatus = false, Region = "Central", ResType = "Grill"},
                new ResStatus(){ResName = "EatAmAre", ResBranch = "Fashion Mall", QueueStatus = false, Region = "Central", ResType = "Grill"},
                new ResStatus(){ResName = "EatAmAre", ResBranch = "Century", QueueStatus = false, Region = "Central", ResType = "Grill"},
                new ResStatus(){ResName = "EatAmAre", ResBranch = "ซอยรางน้ำ", QueueStatus = false, Region = "Central", ResType = "Grill"},
                

        }.ForEach(resst => context.ResStatusEntity.Add(resst));

            context.SaveChanges();

        }
    }
}