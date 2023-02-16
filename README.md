[View in Notion](https://www.notion.so/Platform-Philokalo-0e7e957654e54f2ca185d5f7e11f7af3)

# [Platform] Philokalo (아동 사회성·창의성 발달 플랫폼)

개발 환경: AmazonS3, C#, Docker, Jenkins, MediaPipe, MySQL, Photon, SpringCloudConfig, Unity, pandas

기간 (Period): 2022년 10월 5일 → 2022년 12월 1일

작업 배경: 메타버스 아카데미 1기 동료학습 과정

작업 인원: 6인 (XR 2인, Network 2인, AI 2인) (본인 소속: XR)

장르: 3D, PC

프로젝트 요약: 상호작용 통한 아동 사회성 발달 및 활동일지 기반 부모의 자녀 지도 조력 플랫폼 개발 (XR-Network-AI 협업 프로젝트)

## 📽️ 영상

[https://youtu.be/0MR_tqWDDv0](https://youtu.be/0MR_tqWDDv0)

## ✒️ 기획

<aside>
💡 **Philokalo**는 라틴어로는 어린아이를, 그리스어로는 친구를 뜻하는 Philos(φίλος)와 멋지다는 뜻의 그리스어 Kallos(κάλλος)를 융합한 어휘로, 당신의 아이가 **멋있는 아이**가 되기를 바라는 마음에서 비롯된 **메타버스 창의력 놀이터** 입니다.
아이들은 메타버스 안에서의 상호작용을 통해 **사회성**과 **창의력**을 기를 수 있고, 부모들은 자녀의 활동 분석 데이터를 통하여 **자녀의 행동 양식을 파악**하고 지도 방향성을 설정할 수 있게 됩니다.

</aside>

[필로칼로 기획안.pdf](%5BPlatform%5D%20Philokalo%20(%E1%84%8B%E1%85%A1%E1%84%83%E1%85%A9%E1%86%BC%20%E1%84%89%E1%85%A1%E1%84%92%E1%85%AC%E1%84%89%E1%85%A5%E1%86%BC%C2%B7%E1%84%8E%E1%85%A1%E1%86%BC%E1%84%8B%E1%85%B4%E1%84%89%E1%85%A5%E1%86%BC%20%E1%84%87%E1%85%A1%E1%86%AF%E1%84%83%E1%85%A1%200e7e957654e54f2ca185d5f7e11f7af3/%25ED%2595%2584%25EB%25A1%259C%25EC%25B9%25BC%25EB%25A1%259C_%25EA%25B8%25B0%25ED%259A%258D%25EC%2595%2588.pdf)

- 최종(베타타입) 기획안 Drive
    
    [https://drive.google.com/drive/folders/1lPoUhEM_9STN5w2-lgxVUUyidI3OAopo](https://drive.google.com/drive/folders/1lPoUhEM_9STN5w2-lgxVUUyidI3OAopo) 
    

## 🌐 융합 구조도

<aside>
💡 담당 파트: **XR (Unity & Photon)**

</aside>

![20221203_210005.png](%5BPlatform%5D%20Philokalo%20(%E1%84%8B%E1%85%A1%E1%84%83%E1%85%A9%E1%86%BC%20%E1%84%89%E1%85%A1%E1%84%92%E1%85%AC%E1%84%89%E1%85%A5%E1%86%BC%C2%B7%E1%84%8E%E1%85%A1%E1%86%BC%E1%84%8B%E1%85%B4%E1%84%89%E1%85%A5%E1%86%BC%20%E1%84%87%E1%85%A1%E1%86%AF%E1%84%83%E1%85%A1%200e7e957654e54f2ca185d5f7e11f7af3/20221203_210005.png)

![▲ (위) ① Unity에서 웹캠 순간 캡처 이미지를 multipart/form-data 형식으로 Network의 서버에 보내고 Network에서는 해당 파일의 형식을 검증한 뒤 이를 AI 서버로 송신합니다. ② AI에서 CNN을 기반으로 자체 구축한 분류모델은 이미지 분석 결과 가장 유사한 캐릭터 얼굴형을 반환하고 해당 결과는 역으로 Network 서버를 거쳐 Unity에 전달됩니다. ③ Unity 단에서는 포톤 서버를 통한 캐릭터 생성 시 Resources 폴더에 미리 저장해 둔 fbx파일을 이용하여 전달받은 결과와 일치하는 얼굴형을 지닌 캐릭터를 생성합니다.](%5BPlatform%5D%20Philokalo%20(%E1%84%8B%E1%85%A1%E1%84%83%E1%85%A9%E1%86%BC%20%E1%84%89%E1%85%A1%E1%84%92%E1%85%AC%E1%84%89%E1%85%A5%E1%86%BC%C2%B7%E1%84%8E%E1%85%A1%E1%86%BC%E1%84%8B%E1%85%B4%E1%84%89%E1%85%A5%E1%86%BC%20%E1%84%87%E1%85%A1%E1%86%AF%E1%84%83%E1%85%A1%200e7e957654e54f2ca185d5f7e11f7af3/20221203_205941.png)

▲ (위) ① Unity에서 웹캠 순간 캡처 이미지를 multipart/form-data 형식으로 Network의 서버에 보내고 Network에서는 해당 파일의 형식을 검증한 뒤 이를 AI 서버로 송신합니다. ② AI에서 CNN을 기반으로 자체 구축한 분류모델은 이미지 분석 결과 가장 유사한 캐릭터 얼굴형을 반환하고 해당 결과는 역으로 Network 서버를 거쳐 Unity에 전달됩니다. ③ Unity 단에서는 포톤 서버를 통한 캐릭터 생성 시 Resources 폴더에 미리 저장해 둔 fbx파일을 이용하여 전달받은 결과와 일치하는 얼굴형을 지닌 캐릭터를 생성합니다.

![▲ (위) ① Unity에서 10초 마다 및 이동 정지 시마다(ADWS 또는 화살표 키를 뗐을 때) 캐릭터의 위치 정보를 Network 서버로 송신하고 이는 AI 서버에 전달됩니다. ② AI에서는 하루동안 축적된 로그를 특정 시간대에 분석하여 해당 결과를 Network 서버로 송신합니다. ③ Network에서는 상기의 위치분석 결과 및 Unity에서 받은 하트공유 데이터 등을 종합하여 자녀의 행동분석 일지를 도출하고 이를 부모계정에 등록된 이메일로 송신합니다.](%5BPlatform%5D%20Philokalo%20(%E1%84%8B%E1%85%A1%E1%84%83%E1%85%A9%E1%86%BC%20%E1%84%89%E1%85%A1%E1%84%92%E1%85%AC%E1%84%89%E1%85%A5%E1%86%BC%C2%B7%E1%84%8E%E1%85%A1%E1%86%BC%E1%84%8B%E1%85%B4%E1%84%89%E1%85%A5%E1%86%BC%20%E1%84%87%E1%85%A1%E1%86%AF%E1%84%83%E1%85%A1%200e7e957654e54f2ca185d5f7e11f7af3/20221203_205954.png)

▲ (위) ① Unity에서 10초 마다 및 이동 정지 시마다(ADWS 또는 화살표 키를 뗐을 때) 캐릭터의 위치 정보를 Network 서버로 송신하고 이는 AI 서버에 전달됩니다. ② AI에서는 하루동안 축적된 로그를 특정 시간대에 분석하여 해당 결과를 Network 서버로 송신합니다. ③ Network에서는 상기의 위치분석 결과 및 Unity에서 받은 하트공유 데이터 등을 종합하여 자녀의 행동분석 일지를 도출하고 이를 부모계정에 등록된 이메일로 송신합니다.

[기술 융합 구조도.pdf](%5BPlatform%5D%20Philokalo%20(%E1%84%8B%E1%85%A1%E1%84%83%E1%85%A9%E1%86%BC%20%E1%84%89%E1%85%A1%E1%84%92%E1%85%AC%E1%84%89%E1%85%A5%E1%86%BC%C2%B7%E1%84%8E%E1%85%A1%E1%86%BC%E1%84%8B%E1%85%B4%E1%84%89%E1%85%A5%E1%86%BC%20%E1%84%87%E1%85%A1%E1%86%AF%E1%84%83%E1%85%A1%200e7e957654e54f2ca185d5f7e11f7af3/%25EA%25B8%25B0%25EC%2588%25A0_%25EC%259C%25B5%25ED%2595%25A9_%25EA%25B5%25AC%25EC%25A1%25B0%25EB%258F%2584.pdf)

## ↔ Unity & Network 서버 데이터 송수신 구조도

<aside>
💡 **JWT 인증방식**으로 설계된 서버에 맞춰 **Header**에는 상황에 맞는 토큰(부모토큰/자녀토큰)을, **Body**에는 상황별 필요 데이터를 담아 **POST 방식**으로 통신했습니다. 또한 **싱글톤 패턴**을 활용한 매니저 스크립트를 만들어 토큰, 자녀ID 등 플레이 종료 시점까지 여러 Scene에 걸쳐 유지되어야 하는 정보들을 관리했습니다.

</aside>

![네트워크.drawio.svg](%5BPlatform%5D%20Philokalo%20(%E1%84%8B%E1%85%A1%E1%84%83%E1%85%A9%E1%86%BC%20%E1%84%89%E1%85%A1%E1%84%92%E1%85%AC%E1%84%89%E1%85%A5%E1%86%BC%C2%B7%E1%84%8E%E1%85%A1%E1%86%BC%E1%84%8B%E1%85%B4%E1%84%89%E1%85%A5%E1%86%BC%20%E1%84%87%E1%85%A1%E1%86%AF%E1%84%83%E1%85%A1%200e7e957654e54f2ca185d5f7e11f7af3/%25EB%2584%25A4%25ED%258A%25B8%25EC%259B%258C%25ED%2581%25AC.drawio.svg)

[데이터 송수신 구조도](%5BPlatform%5D%20Philokalo%20(%E1%84%8B%E1%85%A1%E1%84%83%E1%85%A9%E1%86%BC%20%E1%84%89%E1%85%A1%E1%84%92%E1%85%AC%E1%84%89%E1%85%A5%E1%86%BC%C2%B7%E1%84%8E%E1%85%A1%E1%86%BC%E1%84%8B%E1%85%B4%E1%84%89%E1%85%A5%E1%86%BC%20%E1%84%87%E1%85%A1%E1%86%AF%E1%84%83%E1%85%A1%200e7e957654e54f2ca185d5f7e11f7af3/%25EB%2584%25A4%25ED%258A%25B8%25EC%259B%258C%25ED%2581%25AC.drawio%201.svg)

## 👩🏼‍💻 담당 파트 & 업무 분담

<aside>
💡 팀 빌딩 취지 & 업무 진행 방식: 그 동안 동일 전공생(XR)간의 팀프로젝트만 주로 해온 바, 융합 프로젝트는 모든 전공(XR, Network, AI, Creator)과 융합이 가능한 좋은 기회였기에 **최대한 많은 개발 전공(XR, Network, AI)과 협업 가능한 팀**을 우선순위에 두었습니다. 나아가 **담당 파트(XR) 개발을 진행할 시에도 항상 다른 파트(Network, AI)와의 융합을 염두에 둔 설계에 중점을 맞추어 업무를 추진**한 바 있습니다.

</aside>

![PurplePrint XR 업무분담.svg](%5BPlatform%5D%20Philokalo%20(%E1%84%8B%E1%85%A1%E1%84%83%E1%85%A9%E1%86%BC%20%E1%84%89%E1%85%A1%E1%84%92%E1%85%AC%E1%84%89%E1%85%A5%E1%86%BC%C2%B7%E1%84%8E%E1%85%A1%E1%86%BC%E1%84%8B%E1%85%B4%E1%84%89%E1%85%A5%E1%86%BC%20%E1%84%87%E1%85%A1%E1%86%AF%E1%84%83%E1%85%A1%200e7e957654e54f2ca185d5f7e11f7af3/PurplePrint_XR_%25EC%2597%2585%25EB%25AC%25B4%25EB%25B6%2584%25EB%258B%25B4.svg)

![PurplePrint XR 업무분담 (1).svg](%5BPlatform%5D%20Philokalo%20(%E1%84%8B%E1%85%A1%E1%84%83%E1%85%A9%E1%86%BC%20%E1%84%89%E1%85%A1%E1%84%92%E1%85%AC%E1%84%89%E1%85%A5%E1%86%BC%C2%B7%E1%84%8E%E1%85%A1%E1%86%BC%E1%84%8B%E1%85%B4%E1%84%89%E1%85%A5%E1%86%BC%20%E1%84%87%E1%85%A1%E1%86%AF%E1%84%83%E1%85%A1%200e7e957654e54f2ca185d5f7e11f7af3/PurplePrint_XR_%25EC%2597%2585%25EB%25AC%25B4%25EB%25B6%2584%25EB%258B%25B4_(1).svg)

[Philokalo XR 업무분담표.pdf](%5BPlatform%5D%20Philokalo%20(%E1%84%8B%E1%85%A1%E1%84%83%E1%85%A9%E1%86%BC%20%E1%84%89%E1%85%A1%E1%84%92%E1%85%AC%E1%84%89%E1%85%A5%E1%86%BC%C2%B7%E1%84%8E%E1%85%A1%E1%86%BC%E1%84%8B%E1%85%B4%E1%84%89%E1%85%A5%E1%86%BC%20%E1%84%87%E1%85%A1%E1%86%AF%E1%84%83%E1%85%A1%200e7e957654e54f2ca185d5f7e11f7af3/Philokalo_XR_%25EC%2597%2585%25EB%25AC%25B4%25EB%25B6%2584%25EB%258B%25B4%25ED%2591%259C.pdf)

## 🗂️ GitHub Link

[GitHub - LeeHaEun1/Philokalo](https://github.com/LeeHaEun1/Philokalo.git)
