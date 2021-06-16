

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

DROP TABLE IF EXISTS `User`;
CREATE TABLE `User` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '用户ID',
  `Level` int(11) NOT NULL DEFAULT '1' COMMENT '用户级别',
  `Account` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL COMMENT '用户账号',
  `Password` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL COMMENT '用户密码',
  `IsDisable` tinyint(1) NOT NULL DEFAULT '0' COMMENT '是否禁用账号（0:已启用、1:已禁用）',
  `IpAddress` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT '' COMMENT '上次登陆IP地址',
  `SignInTime` bigint(20) NOT NULL DEFAULT '0' COMMENT '上次登陆时间',
  `SignUpTime` bigint(20) NOT NULL DEFAULT '0' COMMENT '用户注册时间',
  PRIMARY KEY (`Id`) USING BTREE,
  UNIQUE KEY `idx_Account` (`Account`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

DROP TABLE IF EXISTS `UserBalance`;
CREATE TABLE `UserBalance` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '用户ID',
  `Credit` decimal(18,5) NOT NULL COMMENT '信用额度',
  `Balance` decimal(18,5) NOT NULL DEFAULT '0.00000' COMMENT '剩余额度',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;


SET FOREIGN_KEY_CHECKS = 1;
