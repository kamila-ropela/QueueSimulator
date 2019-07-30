﻿using QueueSimulator.Database;
using System;

namespace QueueSimulator.Simulation
{
    public class Status
    {
        Random random;

        public void BasedOnPriorityValue(int iteration)
        {
            var patients = PatientsDB.GetDataFromPatientsTable();
            int P1 = 0, P2 = 0, P3 = 0, P4 = 0, P5 = 0;

            foreach (var patient in patients)
            {
                if (patient.Piority == "1" && P1 != 2)
                {
                    PatientsDB.UpdateStatusById(patient.Id, "0");
                    P1++;
                }
                else if(patient.Piority == "2" && P2 != 1)
                {
                    PatientsDB.UpdateStatusById(patient.Id, "0");
                    P2++;
                }
                else if (patient.Piority == "3" && P3 != 1 && iteration % 2 == 0)
                {
                    PatientsDB.UpdateStatusById(patient.Id, "0");
                    P3++;
                }
                else if (patient.Piority == "4" && P4 != 2 && iteration % 3 == 0)
                {
                    PatientsDB.UpdateStatusById(patient.Id, "0");
                    P4++;
                }
                else if (patient.Piority == "5" && P5 != 1  && iteration % 3 == 0)
                {
                    PatientsDB.UpdateStatusById(patient.Id, "0");
                    P5++;
                }

            }
        }

        public void PriorityWithReturnToQuery()
        {

        }

        public void PriorityWithAddToQuery()
        {
            NewPatients newPatients = new NewPatients();
            var newPatientsCount = random.Next(0, 15);
            //newPatients.NewPatientFromDB(newPatientsCount);
            newPatients.GeneratePatientWithRandomData(newPatientsCount);
        }

        public void PriorityWithTwoQuery()
        {

        }
    }
}