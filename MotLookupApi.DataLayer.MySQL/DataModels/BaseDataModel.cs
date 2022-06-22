
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotLookupApi.DataLayer.MySQL.DataModels
{
  public class BaseDataModel
  {
    [Column("id")]
    public int Id { get; set; }
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
  }
}
