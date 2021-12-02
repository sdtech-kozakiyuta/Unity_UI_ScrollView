using System;
using System.Collections.Generic;

public static class DateTimeUtil
{
    public static DateTime GetFirstSunday(int year, int month)
    {
        DateTime result = DateTime.MinValue;
        for (result = new DateTime(year, month, 1); result.Month == month; result = result.AddDays(1))
        {
            if (result.DayOfWeek == DayOfWeek.Sunday)
            {
                return result;
            }
        }
        return result;
    }

    public static int GetNumOfWeeks(DateTime date)
    {
        return GetSundays(date).Length + (GetSundays(date)[0].Day > 1 ? 1 : 0);
    }

    public static DateTime[] GetSundays(DateTime date)
    {
        List<DateTime> days = new List<DateTime>();

        for (DateTime d = new DateTime(date.Year, date.Month, 1);
                                d.Month == date.Month; d = d.AddDays(1))
        {
            if (d.DayOfWeek == DayOfWeek.Sunday)
            {
                days.Add(d);
            }
        }
        return days.ToArray();
    }

    /// <summary>
    /// 月と第何週目かを指定して，その週の一週間に日付リストを取得
    /// </summary>
    /// <param name="date">指定したい月が入ったDateTime型</param>
    /// <param name="weekIndex">第何週目か</param>
    /// <returns></returns>
    public static DateTime[] GetDaysInTheWeek(DateTime date, int weekIndex)
    {
        DateTime[] result = new DateTime[7];
        DateTime[] sundays = GetSundays(date);
        
        // 指定する月の1日が日曜日の場合
        if(sundays[0].Day == 1)
        {
            for(int i = 0; i < result.Length; i++)
            {
                result[i] = sundays[weekIndex].AddDays(i);
            }
        }
        else
        {
            // 1日が日曜日でない指定月の第一週目
            if (weekIndex == 0)
            {
                DateTime[] prevSundays = GetSundays(date.AddMonths(-1));
                for (int i = 0; i < result.Length; i++)
                {
                    result[i] = prevSundays[prevSundays.Length - 1].AddDays(i);
                }
            }
            else
            {
                for (int i = 0; i < result.Length; i++)
                {
                    result[i] = sundays[weekIndex - 1].AddDays(i);
                }
            }
        }
        return result;
    }

    public static DateTime GetBeginningOfTheWeek(DateTime date)
    {
        return date.AddDays(-(int)date.DayOfWeek);
    }

    public static DateTime GetEndOfTheWeek(DateTime date)
    {
        int dayOfWeekNum = Enum.GetValues(typeof(DayOfWeek)).Length;
        return date.AddDays(dayOfWeekNum - (int)date.DayOfWeek - 1);
    }

    public static DateTime GetBeginningOfTheMonth(DateTime date){
        return new DateTime(date.Year, date.Month, 1);
    }

    public static DateTime GetLastSundayOfTheMonth(DateTime date)
    {
        DateTime[] sundays = DateTimeUtil.GetSundays(date);
        return sundays[sundays.Length - 1];
    }
    public static DateTime GetLastDateOfTheMonth(DateTime date)
    {
        DateTime tempDate = DateTimeUtil.GetBeginningOfTheMonth(date);
        return tempDate.AddMonths(1).AddDays(-1);
    }
}
