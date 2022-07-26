
# Softwareentwicklung 2022 - Projekt <!-- omit in toc -->


[![build-and-test](https://github.com/Ifi-Softwareentwicklung-SoSe2022/SWE22_Projekt/actions/workflows/build-and-test.yml/badge.svg?branch=main)](https://github.com/Ifi-Softwareentwicklung-SoSe2022/SWE22_Projekt/actions/workflows/build-and-test.yml)&nbsp;
[![GitHub License](https://img.shields.io/badge/license-MIT-green)](LICENSE)&nbsp;
![visitors](https://visitor-badge.laobi.icu/badge?page_id=Ifi-Softwareentwicklung-SoSe2022/SWE22_Projekt/devlop)&nbsp;

**Übersicht:**

- [Zielstellung](#zielstellung)
- [Aktueller Stand](#aktueller-stand)
  - [UML-Diagramme](#uml-diagramme)
  - [Dokumentation](#dokumentation)
  - [Dependencies](#dependencies)
- [Zeitplan](#zeitplan)
- [Kriterienkatalog](#kriterienkatalog)
  - [Wichtiger Hinweis](#wichtiger-hinweis)

## Zielstellung

>*Transformation von Math.NET Inhalten (Matrizen, Vektoren, Terme usw.) nach Latex*

Das Ergbenis soll ein wiederverwendbare Klassenbibliothek sein, die dem Programmierer die Möglichkeit gibt, mathematische C#-Objekte der Math.NET Bibliothek in LaTeX zu transformieren und als Text zu speichern.

## Aktueller Stand

### UML-Diagramme

![matex.uml](http://www.plantuml.com/plantuml/png/lLNHRk8m47plL_Yng9HlABI6urHe579jfTEfCc43qIKsiZUK2lNVpp5PS3WY8LhH5tRZsPcrFTxU6HGQYpoxkeepOGnxqMAv1CqsLyp-TJeiKPhTAvcayqwW0V4ZqAd4LCxjeiDiB0McyIUxOGPrkU3XhgjF26jiDS372a-eE46WJUiYGz3I5gsWTMGVFItYlu0ZDGF3chVX-2ki8PtfDe-SN8qeBhG6wPBMjpCUhgXyAJHSGklt7r_Dd2NNS3Tdj_19RjrUADiSDZLLAcDrknw53ETIQUYd6V1r34jCbJGMUCmKvIRwIkS2ZHSVeeQvc0F1A3c2Uz4fRdUp3mynzIEWb93DldqK2EiV86QW6n0JiO975OjolxN3NB5DGyb8u29IZlhw6M9KcgtSJ-k40XlhTCroztDAWJy3R8FK8LylbSRzR1B6GMnuz7Qxfhme8ijAvfqDapG3AN9edLtPhba2T_5N63rRl5Fu5JxVGlGQlucJR3drqk_U-gMEk4cd-G0ltq73STCxICjPq_mGt2WwxoMSKDgkpptNYtzJjxTi0TaID4iA6RifQx_LzMwTdZiSDv2lFOsTuBH84j2jiolRMsZgnlv-v1yd347C_kTJ1TOvpozkRiWDFP15xfcb6ditxEcPX-7mWLTfDmZu-86Iw6qITSE8dA5azi4IfNrxTIB-QjUgYk5umiFehTkFUFZsy7J_QyJ7H593z7Z_AUHxY1MbBZtsVPlHAn6Cg3ihJ8t56WI2OOyYWbSsKt6HWqJsdkA2BTp_BchHbUraIqcdMN-Q9DhZW-6uYhq6w_uayCzWi8iuXRrHK4N1cBuDXlqgcZmFIiuxaBCYp_u1)



### Dokumentation

- Dokumentation [öffnen](https://ifi-softwareentwicklung-sose2022.github.io/SWE22_Projekt)
<!--  -->
- als GitHub-Page im Branch [gh-pages](https://github.com/Ifi-Softwareentwicklung-SoSe2022/SWE22_Projekt/tree/gh-pages)
- mit VS Code Extension [Markdown All in One](https://marketplace.visualstudio.com/items?itemName=yzhang.markdown-all-in-one) aus [Dokumentation.md](https://github.com/Ifi-Softwareentwicklung-SoSe2022/SWE22_Projekt/blob/gh-pages/docs/Dokumentation.md) generiert

### Dependencies

- GitHub Dependency Übersicht [öffnen](https://github.com/Ifi-Softwareentwicklung-SoSe2022/SWE22_Projekt/network/dependencies)
<!--  -->
- [CSharpMath](https://www.nuget.org/packages/CSharpMath)
- [Math.NET Numerics](https://www.nuget.org/packages/MathNet.Numerics)
- [Math.NET Symbolics](https://www.nuget.org/packages/MathNet.Symbolics)
- [Microsoft.CodeAnalysis.CSharp.Scripting](https://www.nuget.org/packages/Microsoft.CodeAnalysis.CSharp.Scripting)  

## Zeitplan

|             Datum | Bemerkung                                                                           |
| -----------------:| ----------------------------------------------------------------------------------- |
|     21. Juni 2022 | Rückmeldung der Gruppen zur Teilnahme an der praktischen Prüfungsleistung           |
|                   | Anlegen eines Repositories und Erläuterung Ihrer Zielstellungen im Wiki             |
|  bis 7. Juli 2022 | Spezifikation einer Fragestellung und zugehöriger Softwareentwurf                   |
| bis 14. Juli 2022 | Bestätigung der Idee und des Entwurfes                                              |
|                   | Bearbeitung der Aufgabenstellung in einem GitHub Projekt mit eingeladenen Betreuern |
|     26. Juli 2022 | (Tag der Klausur) Abschluss der Bearbeitung, die Repos werden gespiegelt.           | 

## Kriterienkatalog

In der Bewertung fließen neben der eigentlichen Implementierung insbesondere der Entwurf und der Entwicklungsprozess ein!

| Aspekt             | Maßstäbe                                                                   | Bewertungsgewicht |
| ------------------ | -------------------------------------------------------------------------- | ----------------- |
| Funktionalität     | Komplexität, Originalität, Grundidee                                       | 20 %              |
| Softwareentwurf    | Klassendesign, Dokumentation des methodischen Vorgehens, integrierte Tests | 20 %              |
| Umsetzung          | Lauffähigkeit, Qualität der Implementierung,                               | 30 %              |
| Versionsmanagement | Entwicklungsfluss auf Github, Teamwork                                     | 20 %              |
| Dokumentation      | Tutorial, Beispielcode, Erläuterungen der API                              | 10 %              |

### Wichtiger Hinweis

- in der [Contributors Übersicht](https://github.com/Ifi-Softwareentwicklung-SoSe2022/SWE22_Projekt/graphs/contributors?type=c) ist aus uns unbekannten Gründen zu sehen, dass [@Tom11311](https://github.com/Tom11311) nur sehr wenige *Commits* gemacht hätte, obwohl es eigentlich ca. 14 sind

- auch die angezeigten *Additions* / *Deletions* sind fehrlerhaft, da er u.a. das *Testing* übernommen hat, wie an folgenden *Pull-Requests* und *Issues* zu sehen ist:
  [#1](https://github.com/Ifi-Softwareentwicklung-SoSe2022/SWE22_Projekt/issues/1),
  [#10](https://github.com/Ifi-Softwareentwicklung-SoSe2022/SWE22_Projekt/issues/10),
  [#19](https://github.com/Ifi-Softwareentwicklung-SoSe2022/SWE22_Projekt/issues/19),
  [#2](https://github.com/Ifi-Softwareentwicklung-SoSe2022/SWE22_Projekt/pull/2),
  [#17](https://github.com/Ifi-Softwareentwicklung-SoSe2022/SWE22_Projekt/pull/17),
  [#11](https://github.com/Ifi-Softwareentwicklung-SoSe2022/SWE22_Projekt/pull/11)

- Kurz gesagt: Es soll nicht fälschlicherweise der Eindruck entehen, dass er nichts beigetragen hätte.
