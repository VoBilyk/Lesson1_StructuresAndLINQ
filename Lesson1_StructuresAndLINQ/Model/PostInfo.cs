using Lesson1_StructuresAndLINQ.Model;

namespace Lesson1_StructuresAndLINQ.Model
{
    struct PostInfo
    {
        public Post Post { get; set; }
        public Comment TheLongestComment { get; set; }
        public Comment TheMostLikedComment { get; set; }
        public int? CommentsNumber { get; set; }

        public PostInfo(Post post, Comment theLongestComment, Comment theMostLikedComment, int? commentsNumber)
        {
            Post = post;
            TheLongestComment = theLongestComment;
            TheMostLikedComment = theMostLikedComment;
            CommentsNumber = commentsNumber;
        }
    }
}