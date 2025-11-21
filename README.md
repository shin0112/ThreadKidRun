# ThreadRun

---

## 📑 목차

- [발표 자료 & 문서](#발표-자료--문서)
- [프로젝트 개요](#프로젝트-개요)
- [프로젝트 설명](#프로젝트-설명)
  - [1. Tech Stack](#1-tech-stack)
  - [2. 와이어프레임](#2-와이어프레임)
- [코드 설명](#코드-설명)
  | [1] | [2]| [3] | [4]| [5] |[6] |[7] |[8]|
  | --- | --- |--- | ----|--- |---|---|-|
  | [Managers](#1-managers) | [Map](#2-map) |[Player](#3-player)|[PowerUp](#4-powerup)|[Shop](#5-shop)|[UI](#6-ui)|[Scriptable Objects](#7-scriptable-objects)|[Etc](#8-etc)|
- [기능 설명](#기능-설명)

  | [1]                 | [2]         | [3]             | [4]             | [5]             | [6]                     | [7]                   | [8]             |
  | ------------------- | ----------- | --------------- | --------------- | --------------- | ----------------------- | --------------------- | --------------- |
  | [캐릭터](#1-캐릭터) | [맵](#2-맵) | [버프](#3-버프) | [상점](#4-상점) | [업적](#5-업적) | [튜토리얼](#6-튜토리얼) | [로딩 씬](#7-로딩-씬) | [기타](#8-기타) |

- [트러블슈팅](#트러블슈팅)

---

## [발표 자료 & 문서]

- [PPT (Google Slide)](https://docs.google.com/presentation/d/1gX0cmWSwRL__9r7DIlhT5A_V4X4Zgh1L2HGvngP8aHA/edit?usp=sharing)
- [Figma](https://www.figma.com/embed/interstitial?embed_host=notion&embed_path=%2Fdesign%2FbHepgffXRgt0oKv4IvhP9q%2F%2525EA%2525B2%25258C%2525EC%25259E%252584-%2525EA%2525B0%25259C%2525EB%2525B0%25259C-%2525EC%252588%252599%2525EB%2525A0%2525A8%3Fembed-host%3Dnotion%26footer%3D0%26kind%3Dfile%26node-id%3D0-1%26page-selector%3D1%26theme%3Dsystem%26version%3D2%26viewer%3D1&theme=system&version=2)

## [프로젝트 개요]

| 항목       | 내용                             |
| ---------- | -------------------------------- |
| 프로젝트명 | ThreadRun                        |
| 주제       | 3d 달리기                        |
| 개발 인원  | 총 5명 (개발자)                  |
| 개발 기간  | 2025.11.14 ~ 2025.11.21 (총 7일) |
| 개발 목적  | Unity 3d 실습                    |

## [프로젝트 설명]

### 1. Tech Stack

| 구분            | 기술                                                                                                                  |
| --------------- | --------------------------------------------------------------------------------------------------------------------- |
| Language        | <img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white">                  |
| Framework       | <img src="https://img.shields.io/badge/unity-FFFFFF?style=for-the-badge&logo=unity&logoColor=black">                  |
| IDE             | <img src="https://img.shields.io/badge/Visual%20Studio-5C2D91?style=for-the-badge&logo=visualstudio&logoColor=white"> |
| Version Control | <img src="https://img.shields.io/badge/GitHub-181717?style=for-the-badge&logo=github&logoColor=white">                |
| Design          | <img src="https://img.shields.io/badge/Figma-F24E1E?style=for-the-badge&logo=figma&logoColor=white">                  |
| Documentation   | <img src="https://img.shields.io/badge/Notion-000000?style=for-the-badge&logo=notion&logoColor=white">                |

---

### 2. 와이어프레임

![와이어프레임](Docs/Img/Wireframe.png)

---

## 코드 설명

> 일부 중요 코드만 설명합니다

### 1. Managers

| 코드 이름            | 역할                                         |
| -------------------- | -------------------------------------------- |
| AchievementManager   | 업적 관리                                    |
| AudioManager         | 사운드(배경음, 효과음) 관리                  |
| CharacterSkinManager | ---                                          |
| GameManager          | 코인, 점수, 데이터, 업적, 씬 등 주요 값 관리 |
| PowerUpManager       | 버프 관리                                    |
| UIManager            | UI 관리                                      |

### 2. Map

| 코드 이름      | 역할                         |
| -------------- | ---------------------------- |
| MapMove        | 맵 이동 관리, 생성 파괴 관리 |
| PlayerCollider | 쟁애물 충돌 처리             |
| Coin           | 충돌 시 점수 획득            |

### 3. Player

| 코드 이름             | 역할                                      |
| --------------------- | ----------------------------------------- |
| CustomizingController | 커스터마이징 시 저장되어야 하는 정보 전달 |
| PlayerAnimation       | 플레이어 애니메이션 관리                  |

### 4. PowerUp

| 코드 이름            | 역할                          |
| -------------------- | ----------------------------- |
| InvincibilityPowerUp | 무적 파워업 아이템            |
| PowerUpBase          | 파워업 아이템의 기본 클래스   |
| PowerUpItem          | 필드에 배치되는 파워업 아이템 |
| PowerUpSpawner       | 파워업 랜덤 스포너            |
| SpeedBoostPowerUp    | 스피드 부스트 파워업 아이템   |

### 5. Shop

| 코드 이름      | 역할                        |
| -------------- | --------------------------- |
| CharacterSlot  | 캐릭터 SkinData(SO) 관리    |
| ShopController | 상점 회전, 캐릭터 슬롯 관리 |

### 6. UI

| 코드 이름     | 역할                           |
| ------------- | ------------------------------ |
| AchievementUI | 업적 창                        |
| ButtonUI      | 버튼 관리                      |
| CoinUI        | 코인                           |
| GameOverUI    | 게임오버 창                    |
| PauseUI       | 일시정지 창                    |
| ScoreUI       | 점수 창 (최고 점수, 현재 점수) |
| SettingUI     | 환경 설정 (오디오 조정)        |
| ShopUI        | 상점 버튼                      |
| Tutorial      | 튜토리얼 UI                    |

### 7. Scriptable Objects

| 코드 이름         | 역할                |
| ----------------- | ------------------- |
| AchievementData   | 업적 데이터         |
| CharacterSkinData | 캐릭터 스킨 데이터  |
| PowerUpData       | 파워업(버프) 데이터 |
| SoundData         | 사운드 데이터       |

### 8. Etc

| 코드 이름  | 역할        |
| ---------- | ----------- |
| Define     | 상수 관리   |
| Extensions | 확장 메서드 |
| Logger     | 커스텀 로그 |

---

## [기능 설명]

### 1. 캐릭터

- 움직임
  - 이동 (AD)
  - 점프, 이단 점프 (Space)
  - 슬라이딩 (Control)
- 애니메이션 적용

![Movement](Docs/Img/Movement.gif)

### 2. 맵

- 배경 무한 생성
  - `LastPivot`, `DeadZone`을 통해 맵 무한 생성

![MapLoop](Docs/Img/MapLoop.gif)

- 장애물 충돌

![Collision](Docs/Img/Collision.gif)

- 코인 획득

### 3. 버프

- 버프 적용 (무적, 스피드 업)

![Invincibility](Docs/Img/Invincibility.gif)
![SpeedBoost](Docs/Img/SpeedBoost.gif)

### 4. 상점

- 캐릭터 선택, 구매
- 캐릭터 커스터마이징

![Shop](Docs/Img/Shop.gif)

### 5. 업적

- 업적 해금

![Achievement](Docs/Img/Achievement.jpg)

### 6. 튜토리얼

- 튜툐리얼

![Tutorial](Docs/Img/Tutorial.gif)

### 7. 로딩 씬

![Loading](Docs/Img/Loading.jpg)

### 8. 기타

- 설정
  - 음향 조절 가능

## [트러블슈팅]

> 개발 중 발생한 주요 이슈 및 해결 과정을 정리했습니다.  
> 각 항목은 별도 TIL 또는 블로그 포스트로 링크됩니다.

| 주제                   | 해결 요약                                      | 링크     |
| ---------------------- | ---------------------------------------------- | -------- |
| 파괴된 오브젝트에 접근 | 사용하기 전에 NULL 체크하고 캐싱하기           | [🔗 -]() |
| 태그 오류              | GameManager에서 타입으로 탐색 및 캐싱하여 사용 | [🔗 -]() |

---
