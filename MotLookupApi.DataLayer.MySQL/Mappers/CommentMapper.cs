using System;
using MotLookupApi.DataLayer.MySQL.DataModels;
using MotLookupApi.DataLayer.MySQL.Interfaces;
using MotLookupApi.Framework.Models;

namespace MotLookupApi.DataLayer.MySQL.Mappers
{
  public class CommentMapper : ICommentMapper
  {
    public CommentDataModel Map(Comment comment, int motTestId)
    {
      if (comment == null)
        throw new ArgumentNullException(nameof(comment));
      if (motTestId == default(int))
        throw new ArgumentException("Invalid MotTestId");

      var dm = Map(comment);
      dm.MotTestId = motTestId;

      return dm;
    }

    public CommentDataModel Map(Comment source)
    {
      if (source == null)
        throw new ArgumentNullException(nameof(source));

      return new CommentDataModel
      {
        Id = source.Id,
        Text = source.Text,
        Type = source.Type,
        Dangerous = source.Dangerous        
      };
    }

    public Comment Map(CommentDataModel source)
    {
      if (source == null)
        throw new ArgumentNullException(nameof(source));

      return new Comment
      {
        Id = source.Id,
        Text = source.Text,
        Type = source.Type,
        Dangerous = source.Dangerous
      };
    }
  }
}
