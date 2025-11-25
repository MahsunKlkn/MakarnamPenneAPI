using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IReviewService
    {
        List<Review> GetAllReview(Review review);
        void AddReview(Review review);
        void UpdateReview(Review review);
        void DeleteReview(Review review);

        Review GetById(int id);
    }
}
