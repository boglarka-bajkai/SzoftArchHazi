using SzoftArchHazi.Api;

public class OnDutyDateRepository {

    public static List<OnDutyDate> OnDutyDates = new List<OnDutyDate>();

    private static readonly int[] OnDutyIds = new[]
    {
        0, 0, 2, 2, 1, 3, 4, 6, 7
    };

    private static readonly string[] OnDutyDays = new[]
    {
        "9/1/2023 08:00:0 AM", "9/2/2023 08:00:0 AM", "9/3/2023 08:00:0 AM", "9/4/2023 08:00:0 AM", "3/6/2023 08:00:0 AM", "3/7/2023 08:00:0 AM", "9/1/2022 08:00:0 AM", "9/2/2022 08:00:0 AM", "9/3/2022 08:00:0 AM"
    };

    public static void CreateOnDutyDays()
    {
        for (int i = 0; i < OnDutyDays.Length; i++)
        {
            OnDutyDate onDutyDates = new OnDutyDate();
            onDutyDates.Id = i;
            onDutyDates.OnDutyId = OnDutyIds[i];
            onDutyDates.DutyDay = DateTime.Parse(OnDutyDays[i]);
            OnDutyDates.Add(onDutyDates);
        }
    }

}