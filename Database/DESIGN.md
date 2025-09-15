# Design Document

By Arvin Bohlool

github: Abohlool

## Scope

* This database is designed for a __clinic management system__.It allows the clinic to manage patient, doctor, employee, and visit information.


* The database includes all people who interact with the clinic: both doctors and patients. It also manages visits between patients and doctors.


* Other staff members (e.g., secretaries, administrators), physical rooms, clinic locations, and billing or payment information are not managed in this database.


## Functional Requirements

* Add, update, and query detailed patient and doctor records.
Differentiate between child and adult patients with specific attributes for each group.
Schedule visits between patients and doctors, including rescheduling and cancellation tracking.
Provide convenient views to separately access children and adult patients for administrative ease.


* No management of staff schedules, salaries, or shifts outside doctors.
No clinical documentation such as detailed medical notes or therapy outcomes.
No billing or payment processing.
No tracking of physical rooms or clinic resources assigned to visits.


## Representation

doctors, patients, and visits are captured in a mySQL tables with the following schema.

### Entities

* People: Core entity representing individuals (patients, doctors).
Doctors: Specialized entity referencing people by id.
Patients: Specialized entity referencing people by id, with additional fields distinguishing children and adults.
Visits: Records of appointments between patients and doctors, including cancellation and rescheduling info.


* people: id, name, last_name, age, DOB, education, phone_number, sex.
doctors: just an id referencing people.
patients: id, patient_type, insurance, parent_id, parents_marital_status, parents_job, job, marital_status.
visits: id, doctor_id, patient_id, datetime.


* Use of VARCHAR for names and descriptive text fields to allow flexibility.
Use of ENUM for categorical fields to enforce valid data entries, e.g., sex, patient_type, marital_status.


* Use of FOREIGN KEY constraints to maintain referential integrity between related tables.
Use of PRIMARY KEY and AUTO_INCREMENT where appropriate to uniquely identify records



### Relationships

![ER diagram](diagram.svg)

![ER diagram detailed](diagram_detailed.svg)


Each patient and doctor corresponds to exactly one people entry.
Patients are linked optionally to a single parent (for children) via parent_id.
Visits link a patient and doctor, and may reference a previous visit when rescheduled


## Optimizations

* Views for simplified data access:
    `patients_children` view to list children with parent details.
    `patients_adult` view for adult patient details.
    `visit_history` view to track visits, including rescheduling links.


* Indexes to improve query performance:
    `idx_patient_type` for quick filtering by child/adult.
    `idx_parent_id` to accelerate lookup of child-parent relations.
    `idx_patient_id` and idx_doctor_id for faster joins and filtering on visits


## Limitations

* No clinical records or session details: The system does not store therapy notes or outcomes.
No resource or location management: The database does not track clinic rooms, equipment, or other resources.
Limited staff modeling: Only doctors are represented; no other clinic staff or administrative roles.
No billing or payment functionality.
