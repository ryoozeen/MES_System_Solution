-- MySQL dump 10.13  Distrib 8.0.43, for Win64 (x86_64)
--
-- Host: localhost    Database: mes_solution
-- ------------------------------------------------------
-- Server version	8.0.43

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `equipment`
--

DROP TABLE IF EXISTS `equipment`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `equipment` (
  `equipment_id` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL COMMENT '설비 ID',
  `equipment_name` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL COMMENT '설비명',
  `model` varchar(50) COLLATE utf8mb4_unicode_ci DEFAULT NULL COMMENT '모델명',
  `created_at` datetime DEFAULT CURRENT_TIMESTAMP COMMENT '등록일시',
  PRIMARY KEY (`equipment_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci COMMENT='설비 관리';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `equipment`
--

LOCK TABLES `equipment` WRITE;
/*!40000 ALTER TABLE `equipment` DISABLE KEYS */;
INSERT INTO `equipment` VALUES ('설비1','생산라인 #1','Model-A100','2025-10-21 13:32:44'),('설비2','생산라인 #2','Model-A200','2025-10-21 13:32:44'),('설비3','생산라인 #3','Model-A300','2025-10-21 13:32:44'),('설비4','생산라인 #4','Model-A400','2025-10-21 13:32:44');
/*!40000 ALTER TABLE `equipment` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `equipment_production`
--

DROP TABLE IF EXISTS `equipment_production`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `equipment_production` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT '고유 ID',
  `equipment_id` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL COMMENT '설비 ID',
  `date` date NOT NULL COMMENT '생산 날짜',
  `production_count` int NOT NULL DEFAULT '0' COMMENT '생산 수',
  `faulty_count` int NOT NULL DEFAULT '0' COMMENT '불량 수',
  `faulty_rate` decimal(5,2) NOT NULL DEFAULT '0.00' COMMENT '불량률 (%)',
  `operating_time` int NOT NULL DEFAULT '0' COMMENT '가동 시간 (초)',
  `downtime` int NOT NULL DEFAULT '0' COMMENT '멈춘 시간 (초)',
  `total_time` int NOT NULL DEFAULT '0' COMMENT '총 시간 (초)',
  `operating_rate` decimal(5,2) NOT NULL DEFAULT '0.00' COMMENT '가동률 (%)',
  `current_status` varchar(20) COLLATE utf8mb4_unicode_ci NOT NULL DEFAULT '미가동' COMMENT '현재 상태',
  `updated_at` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '수정일시',
  PRIMARY KEY (`id`),
  UNIQUE KEY `unique_equipment_date` (`equipment_id`,`date`),
  CONSTRAINT `equipment_production_ibfk_1` FOREIGN KEY (`equipment_id`) REFERENCES `equipment` (`equipment_id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci COMMENT='설비별 생산 관리';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `equipment_production`
--

LOCK TABLES `equipment_production` WRITE;
/*!40000 ALTER TABLE `equipment_production` DISABLE KEYS */;
INSERT INTO `equipment_production` VALUES (1,'설비1','2025-10-22',0,0,0.00,0,0,0,0.00,'미가동','2025-10-22 09:00:49'),(2,'설비2','2025-10-22',0,0,0.00,0,0,0,0.00,'미가동','2025-10-22 09:00:49'),(3,'설비3','2025-10-22',0,0,0.00,0,0,0,0.00,'미가동','2025-10-22 09:00:49'),(4,'설비4','2025-10-22',0,0,0.00,0,0,0,0.00,'미가동','2025-10-22 09:00:49');
/*!40000 ALTER TABLE `equipment_production` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `profile`
--

DROP TABLE IF EXISTS `profile`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `profile` (
  `employee_id` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL COMMENT '사번',
  `name` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL COMMENT '이름',
  `department` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL COMMENT '소속',
  `position` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL COMMENT '직급',
  `equipment_id` varchar(50) COLLATE utf8mb4_unicode_ci DEFAULT NULL COMMENT '담당 설비 ID',
  `role` int NOT NULL DEFAULT '0' COMMENT '권한 (0:일반사원, 1:관리자)',
  `status` varchar(20) COLLATE utf8mb4_unicode_ci NOT NULL DEFAULT '재직중' COMMENT '재직 상태 (재직중/퇴사)',
  `created_at` datetime DEFAULT CURRENT_TIMESTAMP COMMENT '등록일시',
  `updated_at` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '수정일시',
  PRIMARY KEY (`employee_id`),
  KEY `equipment_id` (`equipment_id`),
  CONSTRAINT `profile_ibfk_1` FOREIGN KEY (`equipment_id`) REFERENCES `equipment` (`equipment_id`) ON DELETE SET NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci COMMENT='사원 정보 관리';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `profile`
--

LOCK TABLES `profile` WRITE;
/*!40000 ALTER TABLE `profile` DISABLE KEYS */;
INSERT INTO `profile` VALUES ('00001','김관리','관리자','부장',NULL,1,'재직중','2025-10-22 09:00:49','2025-10-22 09:00:49'),('00002','박작업','생산','사원','설비1',0,'재직중','2025-10-22 09:00:49','2025-10-22 09:00:49'),('00003','이담당','생산','사원','설비2',0,'재직중','2025-10-22 09:00:49','2025-10-22 09:00:49');
/*!40000 ALTER TABLE `profile` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `stop_alerts`
--

DROP TABLE IF EXISTS `stop_alerts`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `stop_alerts` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT '고유 ID',
  `equipment_id` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL COMMENT '설비 ID',
  `employee_id` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL COMMENT '작업자 사번',
  `stop_reason` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL COMMENT '멈춤 내용',
  `occurred_at` datetime DEFAULT CURRENT_TIMESTAMP COMMENT '발생일시',
  PRIMARY KEY (`id`),
  KEY `idx_equipment_occurred` (`equipment_id`,`occurred_at`),
  KEY `employee_id` (`employee_id`),
  CONSTRAINT `stop_alerts_ibfk_1` FOREIGN KEY (`equipment_id`) REFERENCES `equipment` (`equipment_id`) ON DELETE CASCADE,
  CONSTRAINT `stop_alerts_ibfk_2` FOREIGN KEY (`employee_id`) REFERENCES `profile` (`employee_id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci COMMENT='멈춤 알림 기록';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `stop_alerts`
--

LOCK TABLES `stop_alerts` WRITE;
/*!40000 ALTER TABLE `stop_alerts` DISABLE KEYS */;
/*!40000 ALTER TABLE `stop_alerts` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `total_production`
--

DROP TABLE IF EXISTS `total_production`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `total_production` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT '고유 ID',
  `date` date NOT NULL COMMENT '날짜',
  `total_production` int NOT NULL DEFAULT '0' COMMENT '총 생산량',
  `total_faulty` int NOT NULL DEFAULT '0' COMMENT '총 불량',
  `total_faulty_rate` decimal(5,2) NOT NULL DEFAULT '0.00' COMMENT '총 불량률 (%)',
  `total_operating_time` int NOT NULL DEFAULT '0' COMMENT '총 가동 시간 (초)',
  `total_time` int NOT NULL DEFAULT '0' COMMENT '총 시간 (초)',
  `total_operating_rate` decimal(5,2) NOT NULL DEFAULT '0.00' COMMENT '총 가동률 (%)',
  `updated_at` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '수정일시',
  PRIMARY KEY (`id`),
  UNIQUE KEY `date` (`date`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci COMMENT='총 생산 관리';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `total_production`
--

LOCK TABLES `total_production` WRITE;
/*!40000 ALTER TABLE `total_production` DISABLE KEYS */;
INSERT INTO `total_production` VALUES (1,'2025-10-22',0,0,0.00,0,0,0.00,'2025-10-22 09:00:49');
/*!40000 ALTER TABLE `total_production` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-10-22  9:01:52
