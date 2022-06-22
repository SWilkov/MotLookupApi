using MotLookupApi.DataLayer.Interfaces;
using MotLookupApi.DataLayer.MySQL.DataModels;
using MotLookupApi.Framework.Models;

namespace MotLookupApi.DataLayer.MySQL.Interfaces
{
  public interface ICommentMapper : IMapper<CommentDataModel, Comment>, 
    IMapper<Comment, CommentDataModel>
  {
    CommentDataModel Map(Comment comment, int motTestId);
  }
}
