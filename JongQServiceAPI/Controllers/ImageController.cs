using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Configuration;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

namespace JongQServiceAPI.Controllers
{
    public class ImageController : ApiController
    {
        //[HttpPost]
        //[ActionName("UploadUserImage")]
        //public async Task<IHttpActionResult> Upload()
        //{
        //    if (!Request.Content.IsMimeMultipartContent("form-data"))
        //        throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

        //    var provider = new MultipartMemoryStreamProvider();
        //    await Request.Content.ReadAsMultipartAsync(provider);

        //    foreach (var f in provider.Contents)
        //    {
        //        CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
        //        ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString);

        //        CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient(); ;
        //        CloudBlobContainer container = blobClient.GetContainerReference("profilecontainer");
        //        container.CreateIfNotExists();
        //        container.SetPermissions(new BlobContainerPermissions
        //        {
        //            PublicAccess =
        //                BlobContainerPublicAccessType.Blob
        //        });

        //        uniqueBlobName = string.Format("profileimages/image_{0}", Guid.NewGuid().ToString());
        //        CloudBlockBlob blob = blobClient.GetContainerReference("profilecontainer").GetBlockBlobReference(uniqueBlobName);

        //        photourl = blob.Uri.AbsoluteUri;

        //        //blob.Properties.ContentType = image.ContentType;
        //        using (var reader = new System.IO.BinaryReader(f.ReadAsStreamAsync().Result))//image.InputStream))
        //        {
        //            blob.UploadFromStream(f.ReadAsStreamAsync().Result);//image.InputStream);
        //        }

        //        return Ok("file saved");


        //    }
        //}
    }
}
