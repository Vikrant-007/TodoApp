using System;
using System.Collections.Generic;
using System.Text;

namespace TodoApp.Application.Models
{
    public class Email
    {
        public string To { get; set; } = default!;
		public string Subject { get; set; } = default!;
		public string Body { get; set; } = default!;
	}
}
