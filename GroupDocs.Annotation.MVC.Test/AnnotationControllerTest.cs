using GroupDocs.Annotation.MVC.Controllers;
using NUnit.Framework;
using System.Web.Routing;
using MvcContrib.TestHelper;
using Huygens;
using System;
using GroupDocs.Annotation.MVC.Products.Annotation.Entity.Web;
using System.Text;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace GroupDocs.Annotation.MVC.Test
{
    [TestFixture]
    public class AnnotationControllerTest
    {
        
        [SetUp]
        public void TestInitialize()
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        [TearDown]
        public void TearDown()
        {
            RouteTable.Routes.Clear();
        }

        [Test]
        public void ViewStatusTest()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "/../../../src";            
            using (var server = new DirectServer(path))
            {
                var request = new SerialisableRequest
                {
                    Method = "GET",
                    RequestUri = "/annotation",
                    Content = null
                };

                var result = server.DirectCall(request);
                Assert.That(result.StatusCode, Is.EqualTo(200));
            }
        }

        [Test]
        public void ViewMapControllerTest()
        {
            "~/annotation".Route().ShouldMapTo<AnnotationController>(x => x.Index());
        }

        [Test]
        public void FileTreeStatusCodeTest()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "/../../../src";
            using (var server = new DirectServer(path))
            {

                AnnotationPostedDataEntity requestData = new AnnotationPostedDataEntity();
                requestData.path = "";

                var request = new SerialisableRequest
                {
                    Method = "POST",
                    RequestUri = "/loadfiletree",
                    Content = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(requestData)),
                    Headers = new Dictionary<string, string>{
                        { "Content-Type", "application/json"},
                        { "Content-Length", JsonConvert.SerializeObject(requestData).Length.ToString()}
                    }
                };

                var result = server.DirectCall(request);
                Assert.That(result.StatusCode, Is.EqualTo(200));
            }
        }
    }
}
