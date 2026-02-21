using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalCardLibrary
{
    /// <summary>
    /// Представляет медицинскую карту, содержащую записи о приемах пациента
    /// </summary>
    public class MedicalCard
    {
        /// <summary>
        /// Массив записей на прием в медицинской карте
        /// </summary>
        public Appointment[] appointments = new Appointment[0];

        /// <summary>
        /// Добавляет запись о приеме в медицинскую карту
        /// </summary>
        /// <param name="appointment">Запись на прием для добавления</param>
        public void Add(Appointment appointment)
        {
            Array.Resize(ref appointments, appointments.Length + 1);
            appointments[appointments.Length - 1] = appointment;
        }

        /// <summary>
        /// Удаляет запись о приеме из медицинской карты
        /// </summary>
        /// <param name="appointment">Запись на прием для удаления</param>
        public void Delete(Appointment appointment)
        {
            int index = Array.IndexOf(appointments, appointment);
            if (index >= 0)
            {
                for (int i = index; i < appointments.Length - 1; i++)
                {
                    appointments[i] = appointments[i + 1];
                }
                Array.Resize(ref appointments, appointments.Length - 1);
            }
        }

        /// <summary>
        /// Получает список специальностей врачей, у которых были приемы в указанную дату
        /// </summary>
        /// <param name="date">Дата для поиска приемов в формате строки</param>
        /// <returns>Массив строк, представляющих специальности врачей</returns>
        public string[] GetDoctorsDate(string date)
        {
            string[] tempResult = new string[0];
            foreach (Appointment appointment in appointments)
            {
                if (appointment.Date == date)
                {
                    Array.Resize(ref tempResult, tempResult.Length + 1);
                    switch (appointment.Doctor.DoctorSpecialty)
                    {
                        case DoctorSpecialty.cardiologist:
                            tempResult[tempResult.Length - 1] = "cardiologist";
                            break;
                        case DoctorSpecialty.dentist:
                            tempResult[tempResult.Length - 1] = "dentist";
                            break;
                        case DoctorSpecialty.psychiatrist:
                            tempResult[tempResult.Length - 1] = "psychiatrist";
                            break;
                        case DoctorSpecialty.oncologist:
                            tempResult[tempResult.Length - 1] = "oncologist";
                            break;
                        case DoctorSpecialty.dermatologist:
                            tempResult[tempResult.Length - 1] = "dermatologist";
                            break;
                    }
                }
            }
            return tempResult;
        }

        /// <summary>
        /// Получает количество пациентов по специальностям врачей за указанную дату
        /// </summary>
        /// <param name="date">Дата для подсчета пациентов в формате строки</param>
        /// <returns>Массив целых чисел с количеством пациентов по специальностям в порядке: кардиолог, стоматолог, психиатр, онколог, дерматолог</returns>
        public int[] GetCountPatientsDoctor(string date)
        {
            int countsCardiologist = 0;
            int countsDentist = 0;
            int countsPsychiatrist = 0;
            int countsOncologist = 0;
            int countsDermatologist = 0;

            foreach (Appointment appointment in appointments)
            {
                if (appointment.Date == date)
                {
                    switch (appointment.Doctor.DoctorSpecialty)
                    {
                        case DoctorSpecialty.cardiologist:
                            countsCardiologist++;
                            break;
                        case DoctorSpecialty.dentist:
                            countsDentist++;
                            break;
                        case DoctorSpecialty.psychiatrist:
                            countsPsychiatrist++;
                            break;
                        case DoctorSpecialty.oncologist:
                            countsOncologist++;
                            break;
                        case DoctorSpecialty.dermatologist:
                            countsDermatologist++;
                            break;
                    }
                }
            }

            return new int[] { countsCardiologist, countsDentist, countsPsychiatrist, countsOncologist, countsDermatologist };
        }

        /// <summary>
        /// Вычисляет среднее количество пациентов в день для каждой специальности врача
        /// </summary>
        /// <returns>Массив вещественных чисел, содержащий среднее количество пациентов в день по специальностям в порядке: кардиолог, стоматолог, психиатр, онколог, дерматолог</returns>
        public double[] GetAveragePatientsPerDay()
        {
            double[] result = new double[5];
            DoctorSpecialty[] specialties = Enum.GetValues<DoctorSpecialty>();

            for (int i = 0; i < specialties.Length; i++)
            {
                string[] uniqueDates = new string[0];
                int[] patientCounts = new int[0];

                foreach (Appointment appointment in appointments)
                {
                    if (appointment.Doctor.DoctorSpecialty == specialties[i])
                    {
                        int dateIndex = Array.IndexOf(uniqueDates, appointment.Date);
                        if (dateIndex == -1)
                        {
                            Array.Resize(ref uniqueDates, uniqueDates.Length + 1);
                            Array.Resize(ref patientCounts, patientCounts.Length + 1);
                            uniqueDates[uniqueDates.Length - 1] = appointment.Date;
                            patientCounts[patientCounts.Length - 1] = 1;
                        }
                        else
                        {    
                            patientCounts[dateIndex]++;
                        }
                    }
                }

                if (patientCounts.Length > 0)
                {
                    double sum = 0;
                    foreach (int count in patientCounts)
                    {
                        sum += count;
                    }
                    result[i] = Math.Round(sum / patientCounts.Length, 2);
                }
                else
                {
                    result[i] = 0;
                }
            }

            return result;
        }
    }
}