using Business.Abstract;
using DataAccessLayer.Abstarct;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ReviewManager : IReviewService
    {
        private readonly IReviewDal _reviewDal;

        public ReviewManager(IReviewDal reviewDal)
        {
            _reviewDal = reviewDal;
        }
        public void AddReview(Review review)
        {
            _reviewDal.Insert(review);
        }

        public void DeleteReview(Review review)
        {
            _reviewDal.Delete(review);
        }

        public List<Review> GetAllReview(Review review)
        {
            return _reviewDal.GetListAll();
        }

        public Review GetById(int id)
        {
            return _reviewDal.GetById(id);
        }

        public void UpdateReview(Review review)
        {
            _reviewDal.Update(review);
        }
    }
}
