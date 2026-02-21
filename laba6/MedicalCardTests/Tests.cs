using MedicalCardLibrary;

namespace MedicalCardTests
{
    [TestClass]
    public class MedicalCardTests
    {
        [TestMethod]
        public void TestPatientConstructor()
        {
            // Arrange & Act
            string patientName = "Иван Иванов";
            Patient patient = new Patient(patientName);

            // Assert
            Assert.AreEqual(patientName, patient.Name);
        }

        [TestMethod]
        public void TestDoctorConstructor()
        {
            // Arrange & Act
            string doctorName = "Доктор Смирнов";
            DoctorSpecialty specialty = DoctorSpecialty.cardiologist;
            Doctor doctor = new Doctor(doctorName, specialty);

            // Assert
            Assert.AreEqual(doctorName, doctor.Name);
            Assert.AreEqual(specialty, doctor.DoctorSpecialty);
        }

        [TestMethod]
        public void TestAppointmentConstructor()
        {
            // Arrange
            DateTime date = new DateTime(2024, 1, 15);
            Patient patient = new Patient("Пациент");
            Doctor doctor = new Doctor("Доктор", DoctorSpecialty.dentist);

            // Act
            Appointment appointment = new Appointment(date, doctor, patient);

            // Assert
            Assert.AreEqual(date.ToShortDateString(), appointment.Date);
            Assert.AreEqual(patient, appointment.Patient);
            Assert.AreEqual(doctor, appointment.Doctor);
        }

        [TestMethod]
        public void TestMedicalCardAddAppointment()
        {
            // Arrange
            MedicalCard medicalCard = new MedicalCard();
            DateTime date = new DateTime(2024, 1, 15);
            Patient patient = new Patient("Пациент");
            Doctor doctor = new Doctor("Доктор", DoctorSpecialty.psychiatrist);
            Appointment appointment = new Appointment(date, doctor, patient);

            // Act
            medicalCard.Add(appointment);

            // Assert
            Assert.AreEqual(1, medicalCard.appointments.Length);
            Assert.AreEqual(appointment, medicalCard.appointments[0]);
        }

        [TestMethod]
        public void TestMedicalCardDeleteAppointment()
        {
            // Arrange
            MedicalCard medicalCard = new MedicalCard();
            DateTime date = new DateTime(2024, 1, 15);
            Patient patient = new Patient("Пациент");
            Doctor doctor = new Doctor("Доктор", DoctorSpecialty.oncologist);
            Appointment appointment = new Appointment(date, doctor, patient);

            medicalCard.Add(appointment);
            Assert.AreEqual(1, medicalCard.appointments.Length);

            // Act
            medicalCard.Delete(appointment);

            // Assert
            Assert.AreEqual(0, medicalCard.appointments.Length);
        }

        [TestMethod]
        public void TestGetDoctorsDate_WithMatchingDate()
        {
            // Arrange
            MedicalCard medicalCard = new MedicalCard();
            string targetDate = "15.01.2024";

            // Добавляем записи на разные даты
            medicalCard.Add(new Appointment(new DateTime(2024, 1, 15),
                new Doctor("Кардиолог", DoctorSpecialty.cardiologist),
                new Patient("Пациент1")));

            medicalCard.Add(new Appointment(new DateTime(2024, 1, 15),
                new Doctor("Стоматолог", DoctorSpecialty.dentist),
                new Patient("Пациент2")));

            medicalCard.Add(new Appointment(new DateTime(2024, 1, 16),
                new Doctor("Дерматолог", DoctorSpecialty.dermatologist),
                new Patient("Пациент3")));

            // Act
            string[] doctors = medicalCard.GetDoctorsDate(targetDate);

            // Assert
            Assert.AreEqual(2, doctors.Length);
            CollectionAssert.Contains(doctors, "cardiologist");
            CollectionAssert.Contains(doctors, "dentist");
            CollectionAssert.DoesNotContain(doctors, "dermatologist");
        }

        [TestMethod]
        public void TestGetDoctorsDate_WithNoMatchingDate()
        {
            // Arrange
            MedicalCard medicalCard = new MedicalCard();
            string nonExistentDate = "31.12.2024";

            medicalCard.Add(new Appointment(new DateTime(2024, 1, 15),
                new Doctor("Доктор", DoctorSpecialty.cardiologist),
                new Patient("Пациент")));

            // Act
            string[] doctors = medicalCard.GetDoctorsDate(nonExistentDate);

            // Assert
            Assert.AreEqual(0, doctors.Length);
        }

        [TestMethod]
        public void TestGetCountPatientsDoctor()
        {
            // Arrange
            MedicalCard medicalCard = new MedicalCard();
            string targetDate = "15.01.2024";

            // Добавляем записи на целевую дату
            medicalCard.Add(new Appointment(new DateTime(2024, 1, 15),
                new Doctor("Кардиолог1", DoctorSpecialty.cardiologist),
                new Patient("Пациент1")));

            medicalCard.Add(new Appointment(new DateTime(2024, 1, 15),
                new Doctor("Кардиолог2", DoctorSpecialty.cardiologist),
                new Patient("Пациент2")));

            medicalCard.Add(new Appointment(new DateTime(2024, 1, 15),
                new Doctor("Стоматолог", DoctorSpecialty.dentist),
                new Patient("Пациент3")));

            // Добавляем запись на другую дату (не должна учитываться)
            medicalCard.Add(new Appointment(new DateTime(2024, 1, 16),
                new Doctor("Кардиолог3", DoctorSpecialty.cardiologist),
                new Patient("Пациент4")));

            // Act
            int[] counts = medicalCard.GetCountPatientsDoctor(targetDate);

            // Assert
            Assert.AreEqual(5, counts.Length); // 5 специальностей
            Assert.AreEqual(2, counts[0]); // cardiologist
            Assert.AreEqual(1, counts[1]); // dentist
            Assert.AreEqual(0, counts[2]); // psychiatrist
            Assert.AreEqual(0, counts[3]); // oncologist
            Assert.AreEqual(0, counts[4]); // dermatologist
        }

        [TestMethod]
        public void TestGetAveragePatientsPerDay()
        {
            // Arrange
            MedicalCard medicalCard = new MedicalCard();

            // Добавляем записи для кардиолога на разные дни
            medicalCard.Add(new Appointment(new DateTime(2024, 1, 15),
                new Doctor("Кардиолог", DoctorSpecialty.cardiologist),
                new Patient("Пациент1")));

            medicalCard.Add(new Appointment(new DateTime(2024, 1, 15),
                new Doctor("Кардиолог", DoctorSpecialty.cardiologist),
                new Patient("Пациент2")));

            medicalCard.Add(new Appointment(new DateTime(2024, 1, 16),
                new Doctor("Кардиолог", DoctorSpecialty.cardiologist),
                new Patient("Пациент3")));

            // Добавляем записи для стоматолога на один день
            medicalCard.Add(new Appointment(new DateTime(2024, 1, 15),
                new Doctor("Стоматолог", DoctorSpecialty.dentist),
                new Patient("Пациент4")));

            // Act
            double[] averages = medicalCard.GetAveragePatientsPerDay();

            // Assert
            Assert.AreEqual(5, averages.Length); // 5 специальностей
            Assert.AreEqual(1.5, averages[0]); // cardiologist: (2 + 1) / 2 дней = 1.5
            Assert.AreEqual(1.0, averages[1]); // dentist: 1 / 1 день = 1.0
            Assert.AreEqual(0.0, averages[2]); // psychiatrist: 0 пациентов
            Assert.AreEqual(0.0, averages[3]); // oncologist: 0 пациентов
            Assert.AreEqual(0.0, averages[4]); // dermatologist: 0 пациентов
        }

        [TestMethod]
        public void TestMedicalCardMultipleOperations()
        {
            // Arrange
            MedicalCard medicalCard = new MedicalCard();
            Patient patient1 = new Patient("Пациент1");
            Patient patient2 = new Patient("Пациент2");
            Doctor doctor1 = new Doctor("Доктор1", DoctorSpecialty.cardiologist);
            Doctor doctor2 = new Doctor("Доктор2", DoctorSpecialty.dentist);

            Appointment appointment1 = new Appointment(new DateTime(2024, 1, 15), doctor1, patient1);
            Appointment appointment2 = new Appointment(new DateTime(2024, 1, 15), doctor2, patient2);

            // Act & Assert - последовательные операции
            medicalCard.Add(appointment1);
            Assert.AreEqual(1, medicalCard.appointments.Length);

            medicalCard.Add(appointment2);
            Assert.AreEqual(2, medicalCard.appointments.Length);

            medicalCard.Delete(appointment1);
            Assert.AreEqual(1, medicalCard.appointments.Length);
            Assert.AreEqual(appointment2, medicalCard.appointments[0]);

            medicalCard.Delete(appointment2);
            Assert.AreEqual(0, medicalCard.appointments.Length);
        }
    }
}
