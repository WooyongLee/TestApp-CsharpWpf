using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieReservationTest
{
    public class Reservation
    {
        private Customer customer;
        private Screening screening;
        private long fee; // Money
        private int audienceCount;

        public Reservation(Customer customer, Screening screening, long fee, int audienceCount)
        {
            this.customer = customer;
            this.screening = screening;
            this.fee = fee;
            this.audienceCount = audienceCount;
        }
    }

    public class Customer
    {

    }

    public class Movie
    {
        private string title;
        private long runningTime;
        public long Fee
        {
            // GetFee()
            get;
            private set;
        }
        private DiscountPolicy discountPolicy;

        public Movie(string title, long runningTime, long fee, DiscountPolicy discountPolicy)
        {
            this.title = title;
            this.runningTime = runningTime;
            this.Fee = fee;
            this.discountPolicy = discountPolicy;
        }

        public long CalculateMovieFee(Screening screening)
        {
            // 영화 원가와 할인액의 차이를 반환함
            return Fee - discountPolicy.CalculateDiscountAmount(screening);
        }
    }

    // 상영 클래스
    public class Screening
    {
        private Movie movie;
        private int sequence;
        public DateTime WhenScreened
        {
            get; // getStartTime()
            private set;
        }

        public Screening(Movie movie, int sequence, DateTime whenScreened)
        {
            this.movie = movie;
            this.sequence = sequence;
            this.WhenScreened = whenScreened;
        }

        public bool IsSequence(int sequence)
        {
            return this.sequence == sequence;
        }
        
        public long GetMovieFee()
        {
            return movie.Fee;
        }


        public Reservation reserve(Customer customer, int audienceCount)
        {
            return new Reservation(customer, this, calculateFee(audienceCount), audienceCount);
        }

        private long calculateFee(int audienceCount)
        {
            // 영화 값과 관람자 수의 곱으로 전체 요금을 반환함
            return movie.CalculateMovieFee(this) * audienceCount;
        }
    }

    public abstract class DiscountPolicy
    {
        private List<IDiscountCondition> conditions = new List<IDiscountCondition>();

        public DiscountPolicy(IDiscountCondition []conditions)
        {
            this.conditions = conditions.ToList();
        }
            
        private long CalculateDiscountAmount(Screening screening)
        {
            foreach ( IDiscountCondition each in conditions)
            {
                if ( each.isSatisfiedBy(screening))
                {
                    return GetDiscountAmount(screening);
                }
            }

            // 할인 조건 불만족 시 0원 반환
            return 0;
        }

        // 할인이 적용된 액수를 반환하는 추상 함수
        abstract protected long GetDiscountAmount(Screening screening);
    }
    

    public interface IDiscountCondition
    {
        bool isSatisfiedBy(Screening screening);
    }

    public class SequenceCondition : IDiscountCondition
    {
        private int sequence;

        public SequenceCondition(int sequence)
        {
            this.sequence = sequence;
        }

        public bool isSatisfiedBy(Screening screening)
        {
            // 상영 순번이 일치할 경우 할인 가능을 true로 판단함
            return screening.IsSequence(sequence);
        }
    }

    public class PeriodCondition : IDiscountCondition
    {
        // 요일
        private DayOfWeek dayOfWeek;
        private DateTime startTime;
        private DateTime endTime;

        public PeriodCondition(DayOfWeek dayOfWeek, DateTime startTime, DateTime endTime)
        {
            this.dayOfWeek = dayOfWeek;
            this.startTime = startTime;
            this.endTime = endTime;
        }

        public bool isSatisfiedBy(Screening screening)
        {
            return (screening.WhenScreened.DayOfWeek == dayOfWeek)
                && (startTime.CompareTo(screening.WhenScreened.ToLocalTime()) <= 0)
                && (endTime.CompareTo(screening.WhenScreened.ToLocalTime()) >= 0);
        }
    }

    public class AmountDiscountPolicy : DiscountPolicy
    {
        private long discountAmount;

        public AmountDiscountPolicy(long discountAmount, IDiscountCondition[] conditions)
        {
            base(conditions);
            this.discountAmount = discountAmount;
        }
        
        override
        protected long GetDiscountAmount(Screening screening)
        {
            return discountAmount;
        }
    }

    public class PercentDiscountPolicy : DiscountPolicy
    {
        private double percent;

        public PercentDiscountPolicy(double percent, IDiscountCondition[] conditions)
        {
            base(conditions);
            this.percent = percent;
        }

        override
        protected long GetDiscountAmount(Screening screening)
        {
            return (long)(screening.GetMovieFee() * percent);
        }
    }

}
