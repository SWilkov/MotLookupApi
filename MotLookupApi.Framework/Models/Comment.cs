using System;
using System.Collections.Generic;
using System.Text;

namespace MotLookupApi.Framework.Models
{
  public class Comment : Base
  {
    public string? Type { get; set; }
    public string? Text { get; set; }
    public bool Dangerous { get; set; }
  }
}
