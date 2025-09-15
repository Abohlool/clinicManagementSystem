# Design Document

By Arvin Bohlool

github: Abohlool

## Scope

* This database is designed for a __clinic management system__.It allows the clinic to manage patient, doctor, employee, and visit information.

* The system captures the __core people__ who interact with the clinic (patients, doctors, employees) and manages __visits between patients and doctors__.

* It enforces uniqueness for social security numbers and phone numbers, ensuring no duplicates are stored.

## Functional Requirements

* Add, update, and query detailed patient and doctor records.
* Differentiate between __child and adult patients__ with specific attribute.
* Record visits between patients and doctors, including __rescheduling__ and __cancellation__ tracking.
* Provide convenient __views__ for easy access to subsets of data (e.g., child vs adult patients, visit history).

## Representation

doctors, patients, and visits are captured in a MySQL tables with the following schema:

### Entities

* __Person__: Core entity representing individuals with common fields.
  * `id`, `name`, `first_name`, `last_name`, `social_security`, `age`, `DOB`, `phone_number`, `sex`
* __Doctors__: Specialization of `Person`.
  * `id`, `specialization`, `office_number`
* __Patients__: Specialization of `Person`.
  * `id`, `patient_type`, `insurance`, `history`, `last_visit_time`
  * Child-specific fields: `parent_id`, `parents_marital_status`, `parents_job`
  * Adult-specific fields: `job`, `marital_status`
* __Employee__: Specialization of `Person`.
  * `id`, `salary`
* __Visit__: Appointment between a patient and doctor.
  * `id`, `doctor_id`, `patient_id`, `visit_time`, cancellation fields, `rescheduled_from_id`

### Constraints
* __Uniqueness__:
  * `social_security` and `phone_number` are unique per person
  * `office_number` is unique per doctor
* __References__:
  * Doctors, patients, and employees reference `person(id)`.
  * Child patients reference `person` via `parent_id`.
  * Visits reference `doctor_id` and `parent_id`.


## Relationships

![ER diagram](diagram.svg)

![ER diagram detailed](diagram_detailed.svg)

* Each doctor, patient, and employee corresponds to exactly one entry in `person`.
* A __child patient__ may reference a parent (also a `person`).
* A __visit__ links exactly one patient and one doctor.
* Visits can reference an earlier visit if rescheduled.


## Optimizations

* __Views__:
  * `patients_children`: lists children with parent details.
  * `patients_adult`: lists adult patient details.
  * `visit_history`: shows all visits, cancellation, and rescheduling links.

* __Indexes__:
  * `idx_patient_type`: quickly filters child vs adult patients.
  * `idx_parent_id`: speeds up child–parent lookups.
  * `idx_patient_id` and `idx_doctor_id`: improve join performance on visits.



## Limitations
* __No billing or payments__: Insurance processing, invoices, or payment transactions are not managed.
* __No physical resource tracking__: Rooms, medical equipment, or other physical assets are not assigned or reserved in the database.
* __Limited employee modeling__: Employees are only represented by salary; roles, shifts, or responsibilities are not tracked.
* __No administrative staff management__: Secretaries, receptionists, and other non-medical staff are not included.
