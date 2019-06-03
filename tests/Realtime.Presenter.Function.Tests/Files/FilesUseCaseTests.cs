using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Realtime.Presenter.Function.Files;
using Realtime.Presenter.Function.Tests.Common;
using Xunit;

namespace Realtime.Presenter.Function.Tests.Files
{
    public class FilesUseCaseTests : UseCaseTests
    {
        private readonly FilesController _controller;

        public FilesUseCaseTests()
        {
            _controller = Get<FilesController>();
        }

        [Fact]
        public async Task GivenUriWithIndexHtmlWhenGetThenReturnsIndexHtmlFromBlobStorage()
        {
            var blobText = Guid.NewGuid().ToString();
            SetupFileBlob("index.html", blobText);
            
            var request = new HttpRequestMessage(HttpMethod.Get, "https://something.com/api/files?file=index.html");
            var result = (FileContentResult)await _controller.GetFile(request);
            result.FileContents.Should().Equal(Encoding.UTF8.GetBytes(blobText));
            result.ContentType.Should().Be("text/html");
        }

        [Fact]
        public async Task GivenUriWithNoFileParameterWhenGetThenReturnsIndexHtmlFromBlobStorage()
        {
            var blobText = Guid.NewGuid().ToString();
            SetupFileBlob("index.html", blobText);
            
            var request = new HttpRequestMessage(HttpMethod.Get, "https://something.com/api/files");
            var result = (FileContentResult)await _controller.GetFile(request);
            result.FileContents.Should().Equal(Encoding.UTF8.GetBytes(blobText));
            result.ContentType.Should().Be("text/html");
        }
        
        [Fact]
        public async Task GivenUrlWithJavascriptFileWhenGetThenReturnsJavascriptFileFromBlobStorage()
        {
            var blobText = Guid.NewGuid().ToString();
            SetupFileBlob("main.js", blobText);
            
            var request = new HttpRequestMessage(HttpMethod.Get, "https://something.com/api/files?file=main.js");
            var result = (FileContentResult)await _controller.GetFile(request);
            result.FileContents.Should().Equal(Encoding.UTF8.GetBytes(blobText));
            result.ContentType.Should().Be("text/javascript");
        }

        [Fact]
        public async Task GivenUrlWithPngFileWhenGetThenReturnsPngFileFromBlobStorage()
        {
            var blobContents = Guid.NewGuid().ToByteArray();
            SetupFileBlob("something.png", blobContents);
            
            var request = new HttpRequestMessage(HttpMethod.Get, "https://something.com/api/files?file=something.png");
            var result = (FileContentResult)await _controller.GetFile(request);
            result.FileContents.Should().Equal(blobContents);
            result.ContentType.Should().Be("image/png");
        }

        [Fact]
        public async Task GivenImageFileThatDoesNotExistInBlobStorageWhenGetThenReturnsNotFound()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://something.com/api/files?file=something.png");
            var result = await _controller.GetFile(request);
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GivenJavascriptFileThatDoesNotExistInBlobStorageWhenGetThenReturnsNotFound()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://something.com/api/files?file=something.js");
            var result = await _controller.GetFile(request);
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}