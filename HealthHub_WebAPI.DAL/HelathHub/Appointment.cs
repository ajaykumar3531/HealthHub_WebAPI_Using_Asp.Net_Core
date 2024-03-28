using System;
using System.Collections.Generic;

namespace HealthHub_WebAPI.DAL.HelathHub;

public partial class Appointment
{
    public byte[] AppointmentId { get; set; } = null!;

    public byte[] PatientUserId { get; set; } = null!;

    public byte[] DoctorUserId { get; set; } = null!;


    public DateTime Date { get; set; }

    public string Description { get; set; } = null!;

    public DateTime? CreatedDate { get; set; }

    public short Status { get; set; }

    public short AppointmentType { get; set; }

    public short StartTime { get; set; }

    public short EndTime { get; set; }

    public virtual User DoctorUser { get; set; } = null!;

    public virtual User PatientUser { get; set; } = null!;
}
