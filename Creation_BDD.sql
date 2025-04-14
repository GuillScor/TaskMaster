-- phpMyAdmin SQL Dump
-- version 5.1.1deb5ubuntu1
-- https://www.phpmyadmin.net/
--
-- Host: localhost:3306
-- Generation Time: Apr 09, 2025 at 08:19 AM
-- Server version: 8.0.41-0ubuntu0.22.04.1
-- PHP Version: 8.1.2-1ubuntu2.21

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `task`
--

-- --------------------------------------------------------

--
-- Table structure for table `Commentaires`
--

CREATE TABLE `Commentaires` (
  `ID_Commentaire` int NOT NULL,
  `datePost` datetime DEFAULT NULL,
  `contenu` varchar(50) DEFAULT NULL,
  `ID_Utilisateur` int NOT NULL,
  `ID_Tache` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `Etiquette`
--

CREATE TABLE `Etiquette` (
  `ID_Etiquette` int NOT NULL,
  `Nom` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `Attache`
--

CREATE TABLE `Attache` (
  `ID_Tache` int NOT NULL,
  `ID_Etiquette` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `Projet`
--

CREATE TABLE `Projet` (
  `ID_Projet` int NOT NULL,
  `nom` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `Travaille_sur`
--

CREATE TABLE `Travaille_sur` (
  `ID_Utilisateur` int NOT NULL,
  `ID_Projet` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `Tache`
--

CREATE TABLE `Tache` (
  `ID_Tache` int NOT NULL AUTO_INCREMENT,
  `titre` varchar(50) DEFAULT NULL,
  `description` varchar(256) DEFAULT NULL,
  `dateCreation` datetime DEFAULT NULL,
  `echeance` datetime DEFAULT NULL,
  `statut` varchar(50) DEFAULT NULL,
  `priorite` varchar(50) DEFAULT NULL,
  `categorie` varchar(50) DEFAULT NULL,
  `etiquettes` varchar(50) DEFAULT NULL,
  `ID_Projet` int DEFAULT NULL,
  `ID_Tache_Parent` int DEFAULT NULL,
  `ID_Auteur` int DEFAULT NULL,
  `ID_realisateur` int DEFAULT NULL,
  PRIMARY KEY (`ID_Tache`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `Utilisateur`
--

CREATE TABLE `Utilisateur` (
  `ID_Utilisateur` int NOT NULL,
  `nom` varchar(50) DEFAULT NULL,
  `prenom` varchar(50) DEFAULT NULL,
  `email` varchar(50) DEFAULT NULL,
  `motdepasse` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `Commentaires`
--
ALTER TABLE `Commentaires`
  ADD PRIMARY KEY (`ID_Commentaire`),
  ADD KEY `ID_Utilisateur` (`ID_Utilisateur`),
  ADD KEY `ID_Tache` (`ID_Tache`);

--
-- Indexes for table `Etiquette`
--
ALTER TABLE `Etiquette`
  ADD PRIMARY KEY (`ID_Etiquette`);

--
-- Indexes for table `Attache`
--
ALTER TABLE `Attache`
  ADD PRIMARY KEY (`ID_Tache`,`ID_Etiquette`),
  ADD KEY `ID_Etiquette` (`ID_Etiquette`);

--
-- Indexes for table `Projet`
--
ALTER TABLE `Projet`
  ADD PRIMARY KEY (`ID_Projet`);

--
-- Indexes for table `Travaille_sur`
--
ALTER TABLE `Travaille_sur`
  ADD PRIMARY KEY (`ID_Utilisateur`,`ID_Projet`),
  ADD KEY `ID_Projet` (`ID_Projet`);

--
-- Indexes for table `Tache`
--
ALTER TABLE `Tache`
  ADD KEY (`ID_Tache`),
  ADD KEY `ID_Projet` (`ID_Projet`),
  ADD KEY `ID_Tache_Parent` (`ID_Tache_Parent`),
  ADD KEY `ID_Auteur` (`ID_Auteur`),
  ADD KEY `ID_realisateur` (`ID_realisateur`);

--
-- Indexes for table `Utilisateur`
--
ALTER TABLE `Utilisateur`
  ADD PRIMARY KEY (`ID_Utilisateur`);

--
-- Constraints for dumped tables
--

--
-- Constraints for table `Commentaires`
--
ALTER TABLE `Commentaires`
  ADD CONSTRAINT `Commentaires_ibfk_1` FOREIGN KEY (`ID_Utilisateur`) REFERENCES `Utilisateur` (`ID_Utilisateur`),
  ADD CONSTRAINT `Commentaires_ibfk_2` FOREIGN KEY (`ID_Tache`) REFERENCES `Tache` (`ID_Tache`);

--
-- Constraints for table `Attache`
--
ALTER TABLE `Attache`
  ADD CONSTRAINT `Attache_ibfk_1` FOREIGN KEY (`ID_Tache`) REFERENCES `Tache` (`ID_Tache`),
  ADD CONSTRAINT `Attache_ibfk_2` FOREIGN KEY (`ID_Etiquette`) REFERENCES `Etiquette` (`ID_Etiquette`);

--
-- Constraints for table `Travaille_sur`
--
ALTER TABLE `Travaille_sur`
  ADD CONSTRAINT `Travaille_sur_ibfk_1` FOREIGN KEY (`ID_Utilisateur`) REFERENCES `Utilisateur` (`ID_Utilisateur`),
  ADD CONSTRAINT `Travaille_sur_ibfk_2` FOREIGN KEY (`ID_Projet`) REFERENCES `Projet` (`ID_Projet`);

--
-- Constraints for table `Tache`
--
ALTER TABLE `Tache`
  ADD CONSTRAINT `Tache_ibfk_1` FOREIGN KEY (`ID_Projet`) REFERENCES `Projet` (`ID_Projet`),
  ADD CONSTRAINT `Tache_ibfk_2` FOREIGN KEY (`ID_Tache_Parent`) REFERENCES `Tache` (`ID_Tache`),
  ADD CONSTRAINT `Tache_ibfk_3` FOREIGN KEY (`ID_Auteur`) REFERENCES `Utilisateur` (`ID_Utilisateur`),
  ADD CONSTRAINT `Tache_ibfk_4` FOREIGN KEY (`ID_realisateur`) REFERENCES `Utilisateur` (`ID_Utilisateur`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
