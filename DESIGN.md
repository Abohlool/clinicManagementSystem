# Design Document

By Arvin Bohlool

github: Abohlool

## Scope
* This project is a __console-based__ clinic management system written in C#.
* It allows the clinic to manages __patients, doctors, employees, and visits (appointments)__.
* The system is __array-based__ with maximum of __100__ objects per category.
* It manages:
  * Patients (with ID, name, phone number, age, & gender).
  * Doctors (with ID, name, phone number, specialization, & schedule).
  * Employees (with ID, name, phone number, & salary).
  * Visits (appointments connecting patients with doctors and a date within the doctors schedule).
* It does not manage:
  * Billing and payment.
  * Clinic resources.
  * Medical history and details.
  * other staff/employee schedules and shifts.

## Functional Requirements
* Add, remove, search, & display __patients__.
  * Search supported by __binary search__ (on ID) and __linear search__ (on name).
  * Patientsare __sorted by name__ when displayed.
* Add, remove, & display __doctors__.
  * Manage available slots.
  * Show schedule of available slots (when scheduling an appointment).
* Add, remove, & display __employees__.
  * Calculate monthly salary.
* Manage __visits__:
  * Add a visit by selecting a patient, doctor, and available time slot.
  * Cancel (remove) visits.
  * Reschedule visits (change doctor or time).
  * Search visits by patient or date range.

## Representation
The system is represented in C# classes and arrays.

### Entities
* __Person (abstract class)__
  * Base class with common fields: `Name`,  `ID`, `PhoneNumber`.
* __Patient__ (inherits Person)
  * Attributes: `Age`, `Gender`.
* __Doctor__ (inherits Person)
  * Attributes: `Specialization`.
  * Schedule: array of `AvailableTimes`.
* __Employee__ (inherits Person)
  * Attributes: `MonthlySalary`.
* __Visit__
  * Links `Patient` and `Doctor`
  * Stores `VisitTime`.

## Algorithms & Data Structures
* __Arrays__ with max size (`MAX = 100`) store patients, doctors, employees, and visit).
* __Sorting__:
  * Selection sort for patients by name and ID.
* __Searching__:
  * Binary search for patients by ID.
  * Linear search for patients by name
  * Linear search for visits (by patient name or date range)

## Validations & Exceptions
* __Phone numbers__ validated using Regex.
* __Gender__ validated against allowed inputs (`male`, `female`, `m`, `f`).
* Custom exceptions:
  * `InvalidPhoneNumberException`.
  * `InvalidGenderException`.
* Age restricted to valid range (0–120).
* ID restricted to numeric values only.

## Optimizations
* Patients are always sorted by name before display → easier readability.
* Binary search for ID lookup reduces search complexity vs linear search.
* Old appointment slots are removed from doctor’s schedule after booking.

## Limitations
* Data is __not presistent__ (lost on exit).
* Fixed array size (`Max = 100`) → no dynamic expansion.
* Rescheduling does not restore the previous time slot.
* No dedicated HR functionality.
* allows duplicate `ID`s and `PhoneNumbers`s.
