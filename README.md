# SardiNet 🐟

> *Fiska ska vara lätt, in på SardiNet*

## Group Project (Stockholm University)

This project was developed collaboratively as part of a university course assignment at Stockholm University.

**My specific contributions to this project included:**
- SMHI Weather API integration for real-time weather forecasts
- Frontend implementation for map interactions (popups, detailed views for fishing spots)
- Development of fish details pages with database connectivity
- Writing test scripts for system validation and integration testing (collaboratively with another team member)
- Implementation of Agile/Scrum methodology and sprint planning
- Conducted observation interviews and usability testing during the development phase to identify and resolve user interface faults (collaborative team effort)

Additional project work was completed by other team members covering areas such as frontend UI design (MudBlazor, including wireframes, low-fidelity, and high-fidelity prototypes), backend server logic (.NET Aspire), database schema creation and data population, core Google Maps API integration, and coordination of overall user acceptance testing.

Project documentation and report writing were completed collaboratively by all team members.

## 🌟 Höjdpunkter

* 📍 Rekommendationer av fiskeplatser baserat på din position
* 🐟 Förslag på lämpliga fiskedrag utifrån lokala fiskarter
* ☀️ Integrerad väderinformation från SMHI för bättre planering
* ⚠️ Information om fiskeregler och krav på fiskekort
* 🗺️ Samlad information på en plattform

## ℹ️ Översikt

En samlad plattform för nybörjare som vill fiska i Stockholm. Tjänsten visar väderinformation, ger rekommendationer för att komma igång och föreslår fiskeplatser baserat på avstånd från användaren. För varje plats visas lämpliga fiskedrag utifrån vilka fiskarter som finns i området. Plattformen informerar även om lokala regler och om fiskekort krävs för det aktuella vattendraget.

Fiska ska vara lätt, in på SardiNet 😎👍

### ✍️ Författare

Abhinash Arudchelvan, Isabelle Johansson, Karin Nezar Mustafa, Oscar Ringqvist, Sibel Demirkiran, Simon Lundmark, Sofie Edström, Viktor Brane

## 🚀 Användning

Backend
```bash
aspire run
```

Frontend
```bash
cd PVT15_8.Mudweb
dotnet run
```

eller för development
```bash
cd PVT15_8.Mudweb
dotnet watch
```

## ⬇️ Installation

1. Installera [Docker Desktop](https://www.docker.com/products/docker-desktop/)
2. Installera [.NET 10 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/10.0)
3. Installera [Aspire CLI](https://aspire.dev/get-started/install-cli/)

aspire cli bash
```bash
curl -sSL https://aspire.dev/install.sh | bash
```

aspire cli powershell
```powershell
irm https://aspire.dev/install.ps1 | iex
```

## 💭 Feedback

Rapportera buggar och ge feedback: https://sardinet.monimon.org/support
