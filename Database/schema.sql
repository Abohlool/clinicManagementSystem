-- ================================================================== TABLES ==================================================================

-- persons table
CREATE TABLE `person` (
    `id` INT AUTO_INCREMENT,
    `name` VARCHAR(65) NOT NULL,
    `first_name` VARCHAR(32) NOT NULL,
    `last_name` VARCHAR(32) NOT NULL,
    `social_security` INT NOT NULL,
    `age` SMALLINT NOT NULL,
    `DOB` DATE,
    `phone_number` VARCHAR(16),
    `sex` ENUM('male', 'female') NOT NULL,
    PRIMARY KEY(`id`),
    UNIQUE(`social_security`), 
    UNIQUE(`phone_number`)
);

-- doctors table
CREATE TABLE `doctors` (
    `id` INT AUTO_INCREMENT,
    `specialization` VARCHAR(64) NOT NULL,
    `office_number` VARCHAR(16),
    PRIMARY KEY(`id`),
    FOREIGN KEY(`id`) REFERENCES `person`(`id`),
    UNIQUE(`office_number`)
);

-- patients table
CREATE TABLE `patients` (
    `id` INT AUTO_INCREMENT,
    `patient_type` ENUM('child', 'adult') NOT NULL,
    `insurance` VARCHAR(64) NOT NULL,
    `history` TEXT,
    `last_visit_time` DATETIME DEFAULT NULL,

    -- Child-specific fields
    `parent_id` INT,
    `parents_marital_status` ENUM('married', 'divorced', 'deceased'),
    `parents_job` VARCHAR(32),

    -- Adult-specific fields
    `job` VARCHAR(32),
    `marital_status` ENUM('single', 'married', 'divorced', 'deceased'),

    PRIMARY KEY(`id`),
    FOREIGN KEY(`id`) REFERENCES `person`(`id`),
    FOREIGN KEY(`parent_id`) REFERENCES `person`(`id`),
    UNIQUE(`parents_id`)
);

-- employee table
CREATE TABLE `employee` (
    `id` INT AUTO_INCREMENT,
    `salary` UNSIGNED FLOAT(2),
    PRIMARY KEY `id`,
    FOREIGN KEY `id` REFERENCES `person`.`id`
)

-- visits table
CREATE TABLE `visits` (
    `id` INT AUTO_INCREMENT,
    `doctor_id` INT NOT NULL,
    `patient_id` INT NOT NULL,
    `visit_time` TIMESTAMP NOT NULL,

    `is_cancelled` BOOLEAN DEFAULT FALSE,
    `cancellation_reason` TEXT DEFAULT NULL,
    `cancellation_date` DATE DEFAULT NULL,

    `rescheduled_from_id` INT DEFAULT NULL,

    PRIMARY KEY(`id`),
    FOREIGN KEY(`doctor_id`) REFERENCES `doctors`(`id`),
    FOREIGN KEY(`patient_id`) REFERENCES `patients`(`id`)
);

-- ================================================================== VIEWS ==================================================================

-- childrens view
CREATE VIEW `patients_children` AS
SELECT
    `patients`.`id`, `person`.`name`, `person`.`last_name`, `person`.`age`, `person`.`sex`,
    `patients`.`insurance`, `patients`.`parent_id`, `patients`.`parents_marital_status`, `patients`.`parents_job`,
    `parent`.`name` AS 'parent_name', `parent`.`last_name` AS 'parent_last_name'
FROM `patients`
INNER JOIN `person` ON `patients`.`id` = `person`.`id`
LEFT JOIN `person` AS `parent` ON `patients`.`parent_id` = `parent`.`id`
WHERE `patients`.`patient_type` = 'child';

-- adults view
CREATE VIEW `patients_adult` AS
SELECT
    `patients`.`id`, `person`.`name`, `person`.`last_name`, `person`.`age`, `person`.`sex`,
    `patients`.`insurance`, `patients`.`job`, `patients`.`marital_status`
FROM `patients`
INNER JOIN `person` ON `patients`.`id` = `person`.`id`
WHERE `patient_type` = 'adult';

-- visits view
CREATE VIEW `visit_history` AS
SELECT
    `current`.`id` AS `current_visit`,
    `current`.`patient_id`,
    `current`.`doctor_id`,
    `current`.`visit_time`,
    `current`.`is_cancelled`,
    `current`.`rescheduled_from_id`,
    `previous`.`visit_time` AS `previous_visit_time`
FROM `visits` `current`
LEFT JOIN `visits` `previous` ON `current`.`rescheduled_from_id` = `previous`.`id`
INNER JOIN `patients` ON `patients`.`id` = `current`.`patient_id`
INNER JOIN `doctors` ON `doctors`.`id` = `current`.`doctor_id`
ORDER BY `current`.`visit_time` DESC;


-- ================================================================== INDICES ==================================================================

-- patients
CREATE INDEX `idx_patient_type` ON `patients`(`patient_type`);
CREATE INDEX `idx_parent_id` ON `patients`(`parent_id`);

-- visit
CREATE INDEX `idx_patient_id` ON `visits`(`patient_id`);
CREATE INDEX `idx_doctor_id` ON `visits`(`doctor_id`);