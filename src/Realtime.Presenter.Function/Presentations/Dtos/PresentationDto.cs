using System;

namespace Realtime.Presenter.Function.Presentations.Dtos
{
    public class PresentationDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public SlideDto[] Slides { get; set; }
    }
}