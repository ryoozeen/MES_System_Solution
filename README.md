# MES 설비 가동 시뮬레이터 연동 시스템 (MES Solution)

---

## 프로젝트 개요
- 제조 현장의 설비 가동 상태를 실시간 모니터링하고 생산 데이터를 관리하는 MES입니다.
- 서버·관리자 클라이언트·설비 클라이언트로 분리된 아키텍처이며, TCP/IP(JSON) 기반 비동기 통신을 사용합니다.
- 1~2초 주기로 데이터를 갱신하고, 재시작 시 당일 데이터를 복원하여 연속성을 보장합니다.

---

## 개발 기간
- 2025.10.13(월) ~ 2025.10.19(일) [7일]

---

## 기술 스택
- OS: Windows 10/11
- Language: C# 12.0
- Framework: .NET 8.0
- UI: Windows Forms
- DB: MySQL 8.0.43
- Protocol: TCP/IP (JSON 기반)
- IDE: Visual Studio 2022

---

## 팀/담당
- 팀 규모: 개인
- 담당: 전체 설계/구현(서버, 관리자 클라이언트, 설비 클라이언트, DB 스키마, 통신 프로토콜)

---

## 데모
- 이미지: <img width="1000" height="800" alt="MES" src="https://github.com/user-attachments/assets/a1a4221c-1367-4a43-b9e9-42605111cebb" />


- 영상: https://www.youtube.com/watch?v=zDSpIVCrd0k

---

## 실행 환경
- .NET SDK 8.0+
- Visual Studio 2022
- MySQL Server 8.0+

---

## DB 준비(DB_MES.sql)
1) MySQL Workbench 또는 CLI로 스키마 생성 후 스크립트 실행
2) CLI 예시
```bash
mysql -u root -p
CREATE DATABASE IF NOT EXISTS mes CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE mes;
SOURCE "C:/Users/womis/OneDrive/Desktop/LMS7기/Project 05/MES_Solution/MES_System_Solution/DB_MES.sql";
```
- 각 줄 설명
  - `mysql -u root -p`: MySQL 접속
  - `CREATE DATABASE ...`: 스키마 생성(없으면 생성)
  - `USE mes;`: mes 스키마 선택
  - `SOURCE ...`: 프로젝트의 DB 스크립트 실행

---

## 환경변수(예시)
- DB_HOST=127.0.0.1
- DB_NAME=mes
- DB_USER=본인 MySQL 사용자
- DB_PASS=본인 비밀번호
- SERVER_HOST=0.0.0.0
- SERVER_PORT=9000

---

## 빌드/실행
### 1) 서버(MES_Server)
1. Visual Studio로 `MES_Server.sln` 열기
2. 시작 프로젝트: `MES_Server`
3. 실행(F5)
- 동작: TCP 리스너(9000) 시작, DB 연결 확인, 설비 상태 초기화(미가동)

### 2) 관리자 클라이언트(MES_Client)
1. `MES_Client/MES_Client.csproj` 열기
2. 실행(F5)
- 동작: 로그인(role=1 확인), 사원 CRUD, 대시보드(1초 갱신), 생산 현황 조회

### 3) 설비 클라이언트(Equipment_Client)
1. `Equipment_Client/Equipment_Client.csproj` 열기
2. 실행(F5)
- 동작: 생산 시뮬레이션, 상태 보고(가동/정지/보수/점검/고장/멈춤), 데이터 복원

---

## 주요 기능
- 사용자 인증 및 권한 관리
  - 사번 기반 로그인, 관리자(role=1)/일반 사원(role=0) 분리
  - 중복 로그인 방지, 클라이언트 접근 제어
- 사원 관리(CRUD)
  - 등록/수정/삭제, 퇴사 처리(논리 삭제), 설비 배정
- 설비 관리/모니터링
  - 상태: 미가동/가동/정지/멈춤/보수/점검/설비 고장
  - 상태 변경 이벤트 서버 전송 및 기록
- 생산 데이터 관리
  - 1초 단위 생산량 증가, 불량률 계산, 가동·정지 시간 추적
  - 날짜별 UPSERT 저장, 재시작 시 당일 데이터 복원
- 멈춤 알림 관리
  - 설비/사원/사유/발생 시각 기록, 긴급 정지·보수중·점검중 구분
- 대시보드
  - 총 생산량/불량률/가동률, 설비별 상태 색상(초록/빨강/보라/노랑/주황/회색)
  - 1초 주기 실시간 갱신

---

## 동작 개요
1) 로그인: 사번 인증 → 권한 확인 → 중복 로그인 차단 → 세션 등록
2) 설비 시작: 상태 "가동" 전송 → 서버 저장/브로드캐스트 → 대시보드 초록 표시
3) 생산 진행: 1초마다 생산/불량/시간 누적, 상태 변경 시 이벤트 기록
4) 멈춤 발생: 사유 기록(`stop_alerts`), 관리자 화면 알림
5) 재시작: 당일 데이터 복원, 상태 초기화("미가동")

---

## 네트워크/프로토콜(요약)
- JSON 기반 메시지 예시
```json
{
  "type": "login",
  "employeeId": "E1234",
  "client": "manager"  
}
```
- 각 필드 설명
  - `type`: 메시지 종류(예: login)
  - `employeeId`: 사번
  - `client`: 클라이언트 종류(manager/equipment)

```json
{
  "type": "equipment_status",
  "equipmentId": "EQ-01",
  "status": "operating",  
  "timestamp": "2025-10-13T09:00:00Z"
}
```
- 각 필드 설명
  - `type`: 메시지 종류(설비 상태 보고)
  - `equipmentId`: 설비 ID
  - `status`: operating/stopped/paused/maintenance/inspection/failure/idle
  - `timestamp`: ISO 시각

---

## 실행 팁
- MySQL 연결 실패 시: 계정 권한, 방화벽, 포트(3306) 확인
- 서버 포트(9000) 방화벽 예외 추가 필요할 수 있음
- Visual Studio 최초 빌드 시 NuGet 복원 확인

---

## 디렉토리 구조(요약)
```
MES_System_Solution/
  DB_MES.sql
  MES_Server.sln
  MES_Server/
  MES_Client/
  Equipment_Client/
  MES_Protocol/
  Image/
```

---

## DB 테이블(요약)
- `equipment(equipment_id, equipment_name, model, created_at)`
- `profile(employee_id, name, department, position, equipment_id, role, status)`
- `equipment_production(id, equipment_id, date, production_count, faulty_count, operating_time, downtime, operating_rate, ...)`
- `stop_alerts(id, equipment_id, employee_id, stop_reason, occurred_at)`
- `total_production(id, date, total_production, total_faulty, total_operating_time, total_operating_rate, ...)`


