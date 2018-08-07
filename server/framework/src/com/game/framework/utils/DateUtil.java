package com.game.framework.utils;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;
import java.util.TimeZone;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

/**
 * @author zB
 */
public class DateUtil {

    private static Logger logger = LoggerFactory.getLogger(DateUtil.class);

    /**
     * @param dateStringToParse
     * @param format ����"yyyyMMdd_HHmmss"��"yyyy-MM-dd HH:mm:ss"
     * @return
     */
    public static java.util.Date GetDate(String dateStringToParse, String format) {
        java.util.Date date = null;
        try {
            date = new SimpleDateFormat(format).parse(dateStringToParse);
        } catch (ParseException e) {
            logger.error("DateUtil", e);;
        }

        return date;
    }

    /**
     * @param date
     * @param fomat ����"yyyyMMdd_HHmmss"��"yyyy-MM-dd HH:mm:ss"
     * @return
     */
    public static String GetDateString(java.util.Date date, String fomat) {
        // "yyyy-MM-dd HH:mm:ss"
        SimpleDateFormat simpleDateFormat = new SimpleDateFormat(fomat);
        String str = simpleDateFormat.format(date);
        return str;
    }

    /**
     * �õ�һ���ֶ����õ����� do what@param dateStringToParse "yyyyMMdd_HHmmss"
     */
    public static java.util.Date GetDate(String dateStringToParse) {
        if (dateStringToParse == null) {
            return null;
        }

        java.util.Date date = null;
        try {
            date = new SimpleDateFormat("yyyyMMdd_HHmmss").parse(dateStringToParse);
        } catch (ParseException e) {
            logger.error("DateUtil", e);;
        }

        return date;
    }

    public static String GetDateString(java.util.Date date) {
        if (date == null) {
            return null;
        }
        // "yyyy-MM-dd HH:mm:ss"
        SimpleDateFormat simpleDateFormat = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
        String str = simpleDateFormat.format(date);
        return str;
    }

    /**
     * �õ���ǰ�����գ�ʱ�� do what@return
     */
    public static java.util.Date GetCurDate() {
        return new java.util.Date();
    }

    /**
     * �õ�ǰ�������գ�ʱ�� do what@return
     */
    public static java.util.Date GetYesterdayDate() {
        Date curDate = GetCurDate();
        Date yesterdayDate = new Date(curDate.getTime() - 1000 * 3600 * 24);
        yesterdayDate.setHours(0);
        yesterdayDate.setMinutes(0);
        yesterdayDate.setSeconds(0);
        return yesterdayDate;
    }

    /**
     * �õ���ǰ�����գ�ʱ�� �ַ�
     * 
     * @return "yyyy-MM-dd HH:mm:ss"
     */
    public static String GetCurDateString() {
        java.util.Date date = DateUtil.GetCurDate();
        return DateUtil.GetDateString(date);
    }

    // /**
    // * �õ���ǰ�����գ�ʱ�� �ַ�
    // * @return "yyyyMMdd_HHmmss"
    // */
    // public static String GetCurDateStringFormatImg()
    // {
    //// String s = GetCurDateString();
    //// s = s.replaceAll("-", "_").replaceAll(" ", "_").replaceAll(":", "_");
    // SimpleDateFormat simpleDateFormat = new SimpleDateFormat( "yyyyMMdd_HHmmss");
    // String s = simpleDateFormat.format(new java.util.Date());
    // return s;
    // }

    private static final String StarSignStr[] = {"������", "��ţ��", "˫����", "��з��", "ʨ����",
            "��Ů��", "�����", "��Ы��", "������", "ħЫ��", "ˮƿ��", "˫����",};

    public static int CheckAge(int bYear, int bMonth, int bDay) {
        Calendar nowCal = Calendar.getInstance(TimeZone.getTimeZone("GMT+08:00"));
        int year = nowCal.get(Calendar.YEAR);
        int month = nowCal.get(Calendar.MONTH) + 1;
        int day = nowCal.get(Calendar.DAY_OF_MONTH);
        int age = 0;

        if ((bYear < year && bMonth < month) || (bYear < year && bMonth == month && bDay <= day)) {
            age = year - bYear;
        } else if ((bYear < year && bMonth > month)
                || (bYear < year && bMonth == month && bDay > day)) {
            age = year - bYear - 1;
        } else if (bYear >= year) {
            age = 0;
        }

        return age;
    }

    public static String CheckStarSign(int bMonth, int bDay) {
        byte id = -1;

        if ((bMonth == 3 && bDay >= 21) || (bMonth == 4 && bDay <= 19)) {
            id = 0;
        } else if ((bMonth == 4 && bDay >= 20) || (bMonth == 5 && bDay <= 20)) {
            id = 1;
        } else if ((bMonth == 5 && bDay >= 21) || (bMonth == 6 && bDay <= 21)) {
            id = 2;
        } else if ((bMonth == 6 && bDay >= 22) || (bMonth == 7 && bDay <= 22)) {
            id = 3;
        } else if ((bMonth == 7 && bDay >= 23) || (bMonth == 8 && bDay <= 22)) {
            id = 4;
        } else if ((bMonth == 8 && bDay >= 23) || (bMonth == 9 && bDay <= 22)) {
            id = 5;
        } else if ((bMonth == 9 && bDay >= 23) || (bMonth == 10 && bDay <= 23)) {
            id = 6;
        } else if ((bMonth == 10 && bDay >= 24) || (bMonth == 11 && bDay <= 22)) {
            id = 7;
        } else if ((bMonth == 11 && bDay >= 23) || (bMonth == 12 && bDay <= 21)) {
            id = 8;
        } else if ((bMonth == 12 && bDay >= 22) || (bMonth == 1 && bDay <= 19)) {
            id = 9;
        } else if ((bMonth == 1 && bDay >= 20) || (bMonth == 2 && bDay <= 18)) {
            id = 10;
        } else if ((bMonth == 2 && bDay >= 19) || (bMonth == 3 && bDay <= 20)) {
            id = 11;;
        }
        return StarSignStr[id];
    }

    public static long GetLastTime(Date time) {
        Date now = DateUtil.GetCurDate();
        return now.getTime() - time.getTime();
    }

    /**
     * ����ʱ���(����)
     * 
     * @param begin
     * @param end
     * @return
     */
    public static int countTimeMinutes(Date begin_date, Date end_date) {
        if (begin_date == null || end_date == null) {
            return 0;
        }
        int minute = (int) ((end_date.getTime() - begin_date.getTime()) / (1000 * 60));
        return Math.abs(minute);
    }

    public static int countTimeSeconds(Date begin_date, Date end_date) {
        if (begin_date == null || end_date == null) {
            return 0;
        }
        int seconds = (int) (((end_date.getTime() - begin_date.getTime()) / 1000) + 1);
        return Math.abs(seconds);
    }

    /**
     * ����ʱ���(��)
     * 
     * @param date1
     * @param date2
     * @return
     */
    public static int countTimeDay(Date date1, Date date2) {
        if (date1 == null || date2 == null) {
            return 0;
        }
        Calendar aCalendar = Calendar.getInstance();
        aCalendar.setTime(date1);
        int day1 = aCalendar.get(Calendar.DAY_OF_YEAR);
        aCalendar.setTime(date2);
        int day2 = aCalendar.get(Calendar.DAY_OF_YEAR);
        return Math.abs(day1 - day2);
    }

    public static boolean isToday(Date date) {
        if (date == null) {
            return false;
        }

        // "yyyyMMdd_HHmmss"
        String date_str = GetDateString(date);
        String cur_date_str = GetCurDateString();
        if (date_str.substring(0, 8).equals(cur_date_str.substring(0, 8))) {
            return true;
        }
        return false;
    }

    public static boolean isToday(long time) {
        Date date = new Date(time);

        // "yyyyMMdd_HHmmss"
        String date_str = GetDateString(date);
        String cur_date_str = GetCurDateString();
        if (date_str.substring(0, 8).equals(cur_date_str.substring(0, 8))) {
            return true;
        }
        return false;
    }

    public static boolean isOneDayInMonth() {
        Calendar aCalendar = Calendar.getInstance();
        int day = aCalendar.get(Calendar.DAY_OF_MONTH);
        if (day == 1) {
            return true;
        }
        return false;
    }

    /**
     * �Ƚ����������Ƿ�Ϊͬһ��
     */
    public static boolean isSameDay(Date date1, Date date2) {

        if (date1 == null || date2 == null) {
            return false;
        }
        String presentDate_str = GetDateString(date1);

        String prizeDate_str = GetDateString(date2);

        if (presentDate_str.substring(0, 8).equals(prizeDate_str.substring(0, 8))) {

            return true;
        }

        return false;
    }

    /**
     * �ж�һ��ʱ�������ʱ�����
     * 
     * @param start_hour ÿ���x��(24h)
     * @param cycle_seconds ÿ��ʱ������(��)
     * @return
     */
    public static Date[] CurTimeInDuration(int start_hour, int cycle_seconds) {
        Date[] date = new Date[2];

        Date curTime = GetCurDate();
        Calendar cal = Calendar.getInstance();
        cal.set(Calendar.HOUR_OF_DAY, start_hour);
        cal.set(Calendar.MINUTE, 0);
        cal.set(Calendar.SECOND, 0);
        cal.set(Calendar.MILLISECOND, 0);

        // �ȼ��㵽ǰһ�������ʱ���
        Calendar zero_cal = Calendar.getInstance();
        zero_cal.set(Calendar.HOUR_OF_DAY, 0);
        zero_cal.set(Calendar.MINUTE, 0);
        zero_cal.set(Calendar.SECOND, 0);
        zero_cal.set(Calendar.MILLISECOND, 0);
        Date zero_date = zero_cal.getTime();
        while (true) {
            cal.add(Calendar.SECOND, -cycle_seconds);
            Date tmp_date = cal.getTime();
            if (tmp_date.compareTo(zero_date) <= 0) {
                break;
            }
        }

        // ���𼶼�ʱ����ж�
        while (true) {
            cal.add(Calendar.SECOND, cycle_seconds);
            Date schoolStartTime = cal.getTime();
            if (schoolStartTime.after(curTime)) {
                date[1] = schoolStartTime;
                cal.add(Calendar.SECOND, -cycle_seconds);
                date[0] = cal.getTime();
                break;
            }
        }

        return date;
    }

    public static int GetCurDateSecond() {
        return GetDateSecond(new Date());
    }

    public static int GetDateSecond(Date date) {
        return (int) (date.getTime() / 1000);
    }

    public static Date GetDate(int second) {
        long time = second * 1000L;
        return new Date(time);
    }

    public static int GetWeekDay() {
        Calendar cal = Calendar.getInstance();
        cal.setTime(new Date());
        int week_day = cal.get(Calendar.DAY_OF_WEEK);
        if (week_day == Calendar.SUNDAY) {
            return 7;
        }
        return week_day - 1;
    }

    /**
     * 判断当前时间是否在时间段内
     * 
     * @param start_time 例 "12:00:05"
     * @param end_time 例 "12:00:10"
     * @return
     */
    public static boolean inTime(String start_time, String end_time) {
        Date date = new Date();

        try {
            SimpleDateFormat simpleDateFormat = new SimpleDateFormat("yyyyMMdd");
            String today_str = simpleDateFormat.format(date);
            java.util.Date start_date = new SimpleDateFormat("yyyyMMdd HH:mm:ss")
                    .parse(String.format("%s %s", today_str, start_time));

            java.util.Date end_date = new SimpleDateFormat("yyyyMMdd HH:mm:ss")
                    .parse(String.format("%s %s", today_str, end_time));

            if ((date.after(start_date) && date.before(end_date)) || date.equals(start_date)
                    || date.equals(end_date)) {
                return true;
            }
        } catch (ParseException e) {
            logger.error("inTime", e);;
        }
        return false;
    }

    /**
     * 距离结束时间还有多少秒,前提是当前时间必<=结束时间，否则返回-1
     * 
     * @param end_time
     * @return
     */
    public static int leftSeconds(String end_time) {
        Date date = new Date();

        try {
            SimpleDateFormat simpleDateFormat = new SimpleDateFormat("yyyyMMdd");
            String today_str = simpleDateFormat.format(date);

            java.util.Date end_date = new SimpleDateFormat("yyyyMMdd HH:mm:ss")
                    .parse(String.format("%s %s", today_str, end_time));

            if (date.compareTo(end_date) <= 0) {
                return (int) ((end_date.getTime() - date.getTime()) / 1000);
            }
        } catch (ParseException e) {
            logger.error("leftSeconds", e);;
        }
        return -1;
    }
}
