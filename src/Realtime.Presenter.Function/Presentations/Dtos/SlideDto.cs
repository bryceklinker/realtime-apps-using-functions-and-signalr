using System;

namespace Realtime.Presenter.Function.Presentations.Dtos
{
    public class SlideDto
    {
        public Guid Id { get; set; }
        public int Order { get; set; }
        public string Content { get; set; }
    }
}