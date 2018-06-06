using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Cashflow9000.Views
{
    public class DatePicker : TextView, DatePickerDialog.IOnDateSetListener, TimePickerDialog.IOnTimeSetListener
    {
        public event EventHandler DateChanged;

        private DateTime _Date;
        public DateTime Date
        {
            get => _Date;
            set
            {
                _Date = value;
                Text = value.ToString($"MM/dd/yyyy {(ShowTime ? "hh:mm" : "")}", null);
            }
        }

        private bool _ShowTime;
        public bool ShowTime
        {
            get => _ShowTime;
            set
            {
                _ShowTime = value;
                Date = _Date;
            }
        }

    public DatePicker(Context context, IAttributeSet attrs) :
        base(context, attrs)
    {
        Initialize();
    }

    public DatePicker(Context context, IAttributeSet attrs, int defStyle) :
        base(context, attrs, defStyle)
    {
        Initialize();
    }

    private void Initialize()
    {
        ShowTime = true;
        Date = DateTime.Today;
        Clickable = true;
        Click += OnClick;
    }

    private void OnClick(object sender, EventArgs eventArgs)
    {
        DatePickerDialog datePickerDialog = new DatePickerDialog(Context, this, _Date.Year, _Date.Month - 1, _Date.Day);
        datePickerDialog.Show();

        if (!ShowTime) return;
        TimePickerDialog timePickerDialog = new TimePickerDialog(Context, this, _Date.Hour, _Date.Minute, false);
        timePickerDialog.Show();
    }

    public void OnDateSet(Android.Widget.DatePicker view, int year, int month, int dayOfMonth)
    {
        Date = new DateTime(year, month + 1, dayOfMonth, _Date.Hour, _Date.Minute, _Date.Second);
        if (!ShowTime) DateChanged?.Invoke(this, EventArgs.Empty);
    }

    public void OnTimeSet(TimePicker view, int hourOfDay, int minute)
    {
        Date = new DateTime(_Date.Year, _Date.Month, _Date.Day, hourOfDay, minute, 0);
        DateChanged?.Invoke(this, EventArgs.Empty);
    }
}
}