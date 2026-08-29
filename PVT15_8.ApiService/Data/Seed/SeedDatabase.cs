using NetTopologySuite;
using NetTopologySuite.Geometries;
using PVT15_8.ApiService.Data.Models;
using PVT15_8.ApiService.Endpoints;
using PVT15_8.Shared.DTOs;

namespace PVT15_8.ApiService.Data.Seed;

public static class SeedDatabase
{
    public static void Seed(ServiceDbContext db, IConfiguration config)
    {
        var baseUrl = config["GatewayUrl"] ?? throw new Exception("GatewayUrl not set");
        var fishImageUrl = baseUrl + "/media/images/fish-species";
        var fishluresUrl = baseUrl + "/media/images/fishing-lure";

        // Fiskarter
        var gädda = db.FishSpecies.FirstOrDefault(f => f.Name == "Gädda");
        if (gädda is null)
        {
            gädda = new FishSpecies { Name = "Gädda", ImageUrl = $"{fishImageUrl}/1" };
            db.FishSpecies.Add(gädda);
        }
        gädda.Description = "Gäddan är en långsmal rovfisk som är känd för sina snabba attacker och vassa tänder. Den lever ofta i vegetationsrika sjöar, vikar och lugna vattendrag där den kan gömma sig och jaga byten. Gäddor äter främst mindre fiskar men kan även ta grodor och andra smådjur. Arten är mycket populär inom sportfiske tack vare sin styrka och aggressivitet vid hugg. Gäddan känns lätt igen på sin avlånga kropp och stora käft.";

        var abborre = db.FishSpecies.FirstOrDefault(f => f.Name == "Abborre");
        if (abborre is null)
        {
            abborre = new FishSpecies { Name = "Abborre", ImageUrl = $"{fishImageUrl}/2" };
            db.FishSpecies.Add(abborre);
        }
        abborre.Description = "Abborren är en av Sveriges vanligaste rovfiskar och finns i de flesta sjöar och kustvatten. Den känns igen på sina mörka tvärränder och sina röda fenor. Abborren jagar ofta i stim och äter småfisk, insekter och kräftdjur. Arten är populär bland sportfiskare eftersom den hugger aggressivt och finns i många vatten. Större abborrar kallas ofta för “randiga rovfiskar” på grund av sitt tydliga mönster.";

        var karp = db.FishSpecies.FirstOrDefault(f => f.Name == "Karp");
        if (karp is null)
        {
            karp = new FishSpecies { Name = "Karp", ImageUrl = $"{fishImageUrl}/3" };
            db.FishSpecies.Add(karp);
        }
        karp.Description = "Karpen är en stor och kraftig fisk som tillhör karpfiskfamiljen och trivs bäst i lugna och varma vatten. Den lever nära botten där den söker efter föda som växter, larver och smådjur. Karpar kan bli mycket gamla och stora, vilket gör dem populära inom specimenfiske. Arten är känd för sin styrka och långa fighter när den fastnar på kroken. Karpen känns igen på sin breda kropp och sina små skäggtömmar runt munnen.";

        var gös = db.FishSpecies.FirstOrDefault(f => f.Name == "Gös");
        if (gös is null)
        {
            gös = new FishSpecies { Name = "Gös", ImageUrl = $"{fishImageUrl}/4" };
            db.FishSpecies.Add(gös);
        }
        gös.Description = "Gösen är en rovfisk med vassa tänder och stora ögon som är anpassade för jakt i mörkt vatten. Den lever ofta i djupa sjöar och större vattendrag där den jagar småfisk nära botten. Gösen är mest aktiv under kväll och natt när den söker efter byte. Arten är mycket uppskattad både som matfisk och sportfisk tack vare sitt fasta och fina kött. Gösen har en lång kropp och liknar en blandning mellan gädda och abborre.";

        var sutare = db.FishSpecies.FirstOrDefault(f => f.Name == "Sutare");
        if (sutare is null)
        {
            sutare = new FishSpecies { Name = "Sutare", ImageUrl = $"{fishImageUrl}/5" };
            db.FishSpecies.Add(sutare);
        }
        sutare.Description = "Sutaren är en karpfisk med rundad kropp och mörkgrön färg som ofta lever i grunda och växtrika sjöar. Den trivs i lugna vatten med mjuk botten där den söker föda bland växter och bottendjur. Sutaren är tålig och klarar vatten med låga syrenivåer bättre än många andra fiskar. Arten är vanligt förekommande under varma sommarmånader när den blir extra aktiv. Sutaren känns igen på sin slemmiga hud och små röda ögon.";

        var mört = db.FishSpecies.FirstOrDefault(f => f.Name == "Mört");
        if (mört is null)
        {
            mört = new FishSpecies { Name = "Mört", ImageUrl = $"{fishImageUrl}/6" };
            db.FishSpecies.Add(mört);
        }
        mört.Description = "Mörten är en vanlig stimfisk i svenska sjöar och vattendrag och fungerar som viktig bytesfisk för rovfiskar. Den lever ofta i stora stim nära vegetation eller bryggor där den söker efter smådjur och växtdelar. Mörten är lätt att fånga och är därför populär bland nybörjare inom mete. Arten känns igen på sina silverfärgade sidor och röda ögon. Trots sin relativt lilla storlek spelar mörten en viktig roll i ekosystemet.";

        var braxen = db.FishSpecies.FirstOrDefault(f => f.Name == "Braxen");
        if (braxen is null)
        {
            braxen = new FishSpecies { Name = "Braxen", ImageUrl = $"{fishImageUrl}/7" };
            db.FishSpecies.Add(braxen);
        }
        braxen.Description = "Braxen är en platt och hög karpfisk som ofta lever nära mjuka och dyiga bottnar. Den söker föda genom att sila bottensediment efter smådjur och organiskt material. Braxen förekommer ofta i stora stim och kan bli förvånansvärt stor i näringsrika vatten. Arten är vanlig i många svenska sjöar och långsamt rinnande åar. Braxen känns lätt igen på sin höga kropp och sina långa fenor.";

        var lake = db.FishSpecies.FirstOrDefault(f => f.Name == "Lake");
        if (lake is null)
        {
            lake = new FishSpecies { Name = "Lake", ImageUrl = $"{fishImageUrl}/8" };
            db.FishSpecies.Add(lake);
        }
        lake.Description = "Laken är en bottenlevande rovfisk med lång kropp som påminner om en ål. Den är mest aktiv under vintern och trivs i kalla och djupa vatten. Laken jagar småfisk och andra bottendjur nära sjöbotten under nattens mörker. Arten är unik eftersom den leker mitt i vintern när många andra fiskar är mindre aktiva. Laken känns igen på sina skäggtömmar och sin marmorerade kropp.";

        var ål = db.FishSpecies.FirstOrDefault(f => f.Name == "Ål");
        if (ål is null)
        {
            ål = new FishSpecies { Name = "Ål", ImageUrl = $"{fishImageUrl}/9" };
            db.FishSpecies.Add(ål);
        }
        ål.Description = "Ålen är en långsmal fisk som vandrar mellan sötvatten och hav under sitt liv. Den lever ofta gömd nära botten där den jagar småfisk och andra djur under natten. Ålen är mycket stark och smidig och kan röra sig genom trånga miljöer och tät vegetation. Arten har en fascinerande livscykel där den vandrar långa sträckor för att leka i havet. Ålen känns igen på sin slingrande kropp och hala hud.";

        var regnbåge = db.FishSpecies.FirstOrDefault(f => f.Name == "Regnbåge");
        if (regnbåge is null)
        {
            regnbåge = new FishSpecies { Name = "Regnbåge", ImageUrl = $"{fishImageUrl}/10" };
            db.FishSpecies.Add(regnbåge);
        }
        regnbåge.Description = "Regnbågen är en populär sportfisk som tillhör laxfamiljen och ofta planteras ut i svenska put-and-take-sjöar. Den är aktiv, stark och känd för sina hopp och snabba rusningar när den krokas. Regnbågen äter insekter, småfisk och kräftdjur och trivs i kalla och syrerika vatten. Arten har fått sitt namn från det färgstarka regnbågsliknande bandet längs sidan. Regnbågen är uppskattad både som matfisk och sportfisk.";

        var röding = db.FishSpecies.FirstOrDefault(f => f.Name == "Röding");
        if (röding is null)
        {
            röding = new FishSpecies { Name = "Röding", ImageUrl = $"{fishImageUrl}/11" };
            db.FishSpecies.Add(röding);
        }
        röding.Description = "Rödingen är en vacker laxfisk som lever i kalla och klara sjöar, ofta i fjällområden. Den trivs bäst i mycket rent och syrerikt vatten där temperaturen är låg. Rödingar äter småfisk, insekter och bottendjur beroende på storlek och miljö. Arten är populär inom sportfiske tack vare sitt starka motstånd och sina färgstarka lekdräkter. Rödingen känns igen på sina ljusa prickar och röda buk under lektid.";

        var öring = db.FishSpecies.FirstOrDefault(f => f.Name == "Öring");
        if (öring is null)
        {
            öring = new FishSpecies { Name = "Öring", ImageUrl = $"{fishImageUrl}/12" };
            db.FishSpecies.Add(öring);
        }
        öring.Description = "Öringen är en rovfisk i laxfamiljen som förekommer både i sjöar, älvar och hav. Den är känd för sin styrka och sina snabba rusningar när den krokas. Öringen äter främst insekter, småfisk och kräftdjur beroende på storlek och miljö. Arten trivs bäst i kalla och syrerika vatten med strömmande partier. Öringen känns igen på sina mörka prickar och sin kraftiga kropp.";

        var harr = db.FishSpecies.FirstOrDefault(f => f.Name == "Harr");
        if (harr is null)
        {
            harr = new FishSpecies { Name = "Harr", ImageUrl = $"{fishImageUrl}/28" };
            db.FishSpecies.Add(harr);
        }
        harr.Description = "Harren är en elegant laxfisk som lever i strömmande och klara vatten. Den är särskilt känd för sin stora och färgglada ryggfena som används för balans i strömmen. Harren äter främst insekter som driver med vattnet och är därför populär inom flugfiske. Arten trivs bäst i kalla älvar och åar med hög vattenkvalitet. Harren är uppskattad för sin vackra färgteckning och sitt lugna men starka motstånd vid fångst.";

        var sik = db.FishSpecies.FirstOrDefault(f => f.Name == "Sik");
        if (sik is null)
        {
            sik = new FishSpecies { Name = "Sik", ImageUrl = $"{fishImageUrl}/28" };
            db.FishSpecies.Add(sik);
        }
        sik.Description = "Siken är en laxfisk som lever i både sjöar och kustvatten och är en viktig matfisk i Norden. Den lever ofta i kallt och klart vatten där den söker smådjur och plankton. Siken förekommer i flera olika former beroende på vattenmiljö och föda. Arten fiskas både kommersiellt och av sportfiskare. Siken känns igen på sin silverglänsande kropp och lilla mun.";

        var skarpsill = db.FishSpecies.FirstOrDefault(f => f.Name == "Skarpsill");
        if (skarpsill is null)
        {
            skarpsill = new FishSpecies { Name = "Skarpsill", ImageUrl = $"{fishImageUrl}/28" };
            db.FishSpecies.Add(skarpsill);
        }
        skarpsill.Description = "Skarpsillen är en liten stimfisk som lever i hav och brackvatten, främst i Östersjön. Den tillhör sillfamiljen och är en viktig födokälla för större rovfiskar och sjöfåglar. Skarpsillen lever i stora stim och livnär sig på plankton och små organismer. Arten används ofta inom livsmedelsindustrin och som fiskmjöl. Skarpsillen känns igen på sin smala silverfärgade kropp.";

        var björkna = db.FishSpecies.FirstOrDefault(f => f.Name == "Björkna");
        if (björkna is null)
        {
            björkna = new FishSpecies { Name = "Björkna", ImageUrl = $"{fishImageUrl}/28" };
            db.FishSpecies.Add(björkna);
        }
        björkna.Description = "Björknan är en mindre karpfisk som liknar braxen men har en mer silverfärgad kropp. Den lever ofta i lugna sjöar och långsamt rinnande vatten med mjuk botten. Björknan söker föda nära botten där den äter smådjur och växtmaterial. Arten förekommer ofta i stim tillsammans med andra karpfiskar. Björknan känns igen på sina stora fjäll och rundade kropp.";

        var ruda = db.FishSpecies.FirstOrDefault(f => f.Name == "Ruda");
        if (ruda is null)
        {
            ruda = new FishSpecies { Name = "Ruda", ImageUrl = $"{fishImageUrl}/17" };
            db.FishSpecies.Add(ruda);
        }
        ruda.Description = "Rudan är en tålig karpfisk som klarar mycket låga syrenivåer och kan leva i små och grunda vatten. Den äter växter, insekter och små bottendjur nära botten. Rudan är känd för sin guldfärgade kropp och sin förmåga att överleva i svåra miljöer. Arten är populär inom mete eftersom den ofta nappar försiktigt men kämpar hårt. Rudan känns igen på sin höga kropp och gyllene färgton.";

        var havsöring = db.FishSpecies.FirstOrDefault(f => f.Name == "Havsöring");
        if (havsöring is null)
        {
            havsöring = new FishSpecies { Name = "Havsöring", ImageUrl = $"{fishImageUrl}/28" };
            db.FishSpecies.Add(havsöring);
        }
        havsöring.Description = "Havsöringen är en vandrande form av öring som lever i havet men återvänder till älvar och åar för att leka. Den är mycket uppskattad bland sportfiskare tack vare sin styrka och snabbhet. Havsöringen jagar småfisk, räkor och andra havslevande djur längs kusten. Arten trivs bäst i kalla och syrerika vatten. Havsöringen känns igen på sin silverblanka kropp och sina mörka prickar.";

        var strömming = db.FishSpecies.FirstOrDefault(f => f.Name == "Strömming");
        if (strömming is null)
        {
            strömming = new FishSpecies { Name = "Strömming", ImageUrl = $"{fishImageUrl}/28" };
            db.FishSpecies.Add(strömming);
        }
        strömming.Description = "Strömmingen är en liten sillfisk som lever i Östersjön och bildar stora stim. Den är en av de viktigaste matfiskarna i norra Europa och används i många traditionella maträtter. Strömmingen lever av plankton och små organismer nära ytan. Arten är viktig både för fisket och som föda för större rovfiskar och sjöfåglar. Strömmingen känns igen på sin silverglänsande kropp och smala form.";

        var sil = db.FishSpecies.FirstOrDefault(f => f.Name == "Sil");
        if (sil is null)
        {
            sil = new FishSpecies { Name = "Sil", ImageUrl = $"{fishImageUrl}/28" };
            db.FishSpecies.Add(sil);
        }
        sil.Description = "Sil är en mindre laxliknande fisk som lever i kalla sjöar och djupa vatten. Den äter främst plankton och små vattenlevande organismer. Arten förekommer ofta i stim och fungerar som bytesfisk för större rovfiskar. Sil är viktig i vissa sjöekosystem där den utgör en stor del av fiskbeståndet. Den känns igen på sin lilla silverfärgade kropp.";

        var sarv = db.FishSpecies.FirstOrDefault(f => f.Name == "Sarv");
        if (sarv is null)
        {
            sarv = new FishSpecies { Name = "Sarv", ImageUrl = $"{fishImageUrl}/21" };
            db.FishSpecies.Add(sarv);
        }
        sarv.Description = "Sarven är en karpfisk som ofta förväxlas med mört men har mer gulröda fenor och en högre kropp. Den lever i grunda och växttäta vatten där den söker föda nära ytan. Sarven äter växter, insekter och smådjur och är aktiv under varma sommardagar. Arten förekommer ofta i stim tillsammans med andra karpfiskar. Sarven känns igen på sina gyllene sidor och röda fenor.";

        var lax = db.FishSpecies.FirstOrDefault(f => f.Name == "Lax");
        if (lax is null)
        {
            lax = new FishSpecies { Name = "Lax", ImageUrl = $"{fishImageUrl}/22" };
            db.FishSpecies.Add(lax);
        }
        lax.Description = "Laxen är en stor rovfisk i laxfamiljen som lever både i hav och älvar. Den föds i sötvatten men vandrar ut till havet där den växer sig stor innan den återvänder för att leka. Laxen är mycket uppskattad både som sportfisk och matfisk tack vare sin styrka och sitt smakrika kött. Arten kan hoppa högt och göra långa rusningar när den krokas. Laxen känns igen på sin kraftiga silverfärgade kropp.";

        var löja = db.FishSpecies.FirstOrDefault(f => f.Name == "Löja");
        if (löja is null)
        {
            löja = new FishSpecies { Name = "Löja", ImageUrl = $"{fishImageUrl}/23" };
            db.FishSpecies.Add(löja);
        }
        löja.Description = "Löjan är en liten silverfärgad karpfisk som ofta lever i stora stim nära ytan. Den är en viktig bytesfisk för många rovfiskar som gädda och gös. Löjan lever av plankton och små insekter i öppet vatten. Arten är snabb och rörlig och syns ofta blänka i solen nära vattenytan. Löjan känns igen på sin smala och mycket glänsande kropp.";

        var gärs = db.FishSpecies.FirstOrDefault(f => f.Name == "Gärs");
        if (gärs is null)
        {
            gärs = new FishSpecies { Name = "Gärs" , ImageUrl = $"{fishImageUrl}/28" };
            db.FishSpecies.Add(gärs);
        }
        gärs.Description = "Gärsen är en liten rovfisk som tillhör abborrfamiljen och lever nära botten. Den äter små bottendjur, insekter och fiskyngel. Gärsen har taggiga fenor och ett randigt mönster som påminner om abborre. Arten är vanlig i både sjöar och kustvatten och förekommer ofta i stora mängder. Gärsen känns igen på sina stora ögon och sina vassa ryggfenor.";

        var kräfta = db.FishSpecies.FirstOrDefault(f => f.Name == "Kräfta");
        if (kräfta is null)
        {
            kräfta = new FishSpecies { Name = "Kräfta", ImageUrl = $"{fishImageUrl}/25" };
            db.FishSpecies.Add(kräfta);
        }
        kräfta.Description = "Kräftan är egentligen inte en fisk utan ett sötvattenslevande kräftdjur som ofta används som bete eller föda för rovfiskar. Den lever nära botten där den gömmer sig bland stenar och vegetation. Kräftor äter växter, smådjur och organiskt material. Arten är viktig i många svenska sjöar och vattendrag och är populär vid kräftfiske. Kräftan känns igen på sina stora klor och hårda skal.";

        var asp = db.FishSpecies.FirstOrDefault(f => f.Name == "Asp");
        if (asp is null)
        {
            asp = new FishSpecies { Name = "Asp", ImageUrl = $"{fishImageUrl}/26" };
            db.FishSpecies.Add(asp);
        }
        asp.Description = "Aspen är en ovanlig karpfisk som jagar småfisk på ett sätt som liknar rovfiskar. Den lever ofta i större sjöar och strömmande vatten där den jagar nära ytan. Aspen är snabb och kraftfull och kan göra explosiva attacker mot bytesfisk. Arten är populär bland sportfiskare som gillar aktivt fiske. Aspen känns igen på sin avlånga silverfärgade kropp.";

        var nors = db.FishSpecies.FirstOrDefault(f => f.Name == "Nors");
        if (nors is null)
        {
            nors = new FishSpecies { Name = "Nors", ImageUrl = $"{fishImageUrl}/27" };
            db.FishSpecies.Add(nors);
        }
        nors.Description = "Norsen är en liten laxliknande fisk som lever i både sjöar och kustvatten. Den är viktig som bytesfisk för större rovfiskar och fåglar. Norsen lever i stim och äter plankton och små organismer i öppet vatten. Arten är känd för sin karakteristiska gurkliknande doft. Norsen känns igen på sin smala silverfärgade kropp.";

        var gers = db.FishSpecies.FirstOrDefault(f => f.Name == "Gers");
        if (gers is null)
        {
            gers = new FishSpecies { Name = "Gers", ImageUrl = $"{fishImageUrl}/28" };
            db.FishSpecies.Add(gers);
        }
        gers.Description = "Gers är en liten karpfisk som påminner om löja till utseendet. Den lever ofta i stim i lugna sjöar och långsamt rinnande vatten. Arten äter små plankton och organismer nära ytan. Gers fungerar som bytesfisk för många större rovfiskar. Den känns igen på sin lilla och smala silverfärgade kropp.";

        var nissöga = db.FishSpecies.FirstOrDefault(f => f.Name == "Nissöga");
        if (nissöga is null)
        {
            nissöga = new FishSpecies { Name = "Nissöga", ImageUrl = $"{fishImageUrl}/28" };
            db.FishSpecies.Add(nissöga);
        }
        nissöga.Description = "Nissöga är en liten bottenlevande fisk med stora ögon och långsmal kropp. Den lever ofta på sandiga eller grusiga bottnar i klara vatten. Arten gömmer sig nära botten och äter små bottendjur och insekter. Nissöga är relativt ovanlig och svår att upptäcka på grund av sitt kamouflage. Den känns igen på sina stora ögon och sitt smala utseende.";

        var siklöja = db.FishSpecies.FirstOrDefault(f => f.Name == "Siklöja");
        if (siklöja is null)
        {
            siklöja = new FishSpecies { Name = "Siklöja", ImageUrl = $"{fishImageUrl}/28" };
            db.FishSpecies.Add(siklöja);
        }
        siklöja.Description = "Siklöjan är en liten sikliknande fisk som lever i kalla och djupa sjöar. Den lever i stora stim och äter främst plankton i öppet vatten. Siklöjan är viktig som föda för större rovfiskar som öring och röding. Arten är också känd för sin rom som används till löjrom. Siklöjan känns igen på sin lilla silverglänsande kropp och sitt stimlevande beteende.";

        // Fishing spots 
        var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(RecommendationEndpoints.SRID);
        var aglasjon = db.FishingSpots.FirstOrDefault(s => s.Name == "Aglasjön");
        if (aglasjon is null)
        {
            aglasjon = new FishingSpot
            {
                Name = "Aglasjön",
                Description = "Sjö",
                Latitude = 59.12222,
                Longitude = 17.63693,
                Depth = 4.5,
                HasRules = true,
                IsForbidden = false,
                IsDeleted = false,
                IsFishingCardRequired = true,
                Location = geometryFactory.CreatePoint(new Coordinate(17.63693, 59.12222))
            };
            db.FishingSpots.Add(aglasjon);
        }

        var bornsjon = db.FishingSpots.FirstOrDefault(s => s.Name == "Bornsjön");
        if (bornsjon is null)
        {
            bornsjon = new FishingSpot
            {
                Name = "Bornsjön",
                Description = "Badplats",
                Latitude = 59.24027,
                Longitude = 17.74350,
                Depth = 0,
                HasRules = false,
                IsForbidden = true,
                IsDeleted = false,
                IsFishingCardRequired = false,
                Location = geometryFactory.CreatePoint(new Coordinate(17.74350, 59.24027))
            };
            db.FishingSpots.Add(bornsjon);
        }
        var Brunnsviken = db.FishingSpots.FirstOrDefault(s => s.Name == "Brunnsviken");
        if (Brunnsviken is null)
        {
            Brunnsviken = new FishingSpot
            {
                Name = "Brunnsviken",
                Description = "Sjö",
                Latitude = 59.3700,
                Longitude = 18.0383,
                Depth = 14,
                HasRules = true,
                IsForbidden = false,
                IsDeleted = false,
                IsFishingCardRequired = false,
                Location = geometryFactory.CreatePoint(new Coordinate(18.0383, 59.3700))
            };
            db.FishingSpots.Add(Brunnsviken);
        }
        var Bällstaån = db.FishingSpots.FirstOrDefault(s => s.Name == "Bällstaån");
        if (Bällstaån is null)
        {
            Bällstaån = new FishingSpot
            {
                Name = "Bällstaån",
                Description = "Å",
                Latitude = 59.37061,
                Longitude = 17.922617,
                Depth = 6,
                HasRules = true,
                IsForbidden = false,
                IsDeleted = false,
                IsFishingCardRequired = false,
                Location = geometryFactory.CreatePoint(new Coordinate(17.922617, 59.37061))
            };
            db.FishingSpots.Add(Bällstaån);
        }
        var Djurgårdsbrunnsviken = db.FishingSpots.FirstOrDefault(s => s.Name == "Djurgårdsbrunnsviken");
        if (Djurgårdsbrunnsviken is null)
        {
            Djurgårdsbrunnsviken = new FishingSpot
            {
                Name = "Djurgårdsbrunnsviken",
                Description = "Sjö",
                Latitude = 59.33130,
                Longitude = 18.09376,
                Depth = 8,
                HasRules = true,
                IsForbidden = false,
                IsDeleted = false,
                IsFishingCardRequired = false,
                Location = geometryFactory.CreatePoint(new Coordinate(18.09376, 59.33130))
            };
            db.FishingSpots.Add(Djurgårdsbrunnsviken);
        }
        var Drevviken = db.FishingSpots.FirstOrDefault(s => s.Name == "Drevviken");
        if (Drevviken is null)
        {
            Drevviken = new FishingSpot
            {
                Name = "Drevviken",
                Description = "Sjö",
                Latitude = 59.211108,
                Longitude = 18.180984,
                Depth = 15.2,
                HasRules = true,
                IsForbidden = false,
                IsDeleted = false,
                IsFishingCardRequired = true,
                Location = geometryFactory.CreatePoint(new Coordinate(18.180984, 59.211108))
            };
            db.FishingSpots.Add(Drevviken);
        }
        var Edsviken = db.FishingSpots.FirstOrDefault(s => s.Name == "Edsviken");
        if (Edsviken is null)
        {
            Edsviken = new FishingSpot
            {
                Name = "Edsviken",
                Description = "Sjö",
                Latitude = 59.415520,
                Longitude = 17.993004,
                Depth = 20,
                HasRules = true,
                IsForbidden = false,
                IsDeleted = false,
                IsFishingCardRequired = false,
                Location = geometryFactory.CreatePoint(new Coordinate(17.993004, 59.415520))
            };
            db.FishingSpots.Add(Edsviken);
        }
        var Ekebysjön = db.FishingSpots.FirstOrDefault(s => s.Name == "Ekebysjön");
        if (Ekebysjön is null)
        {
            Ekebysjön = new FishingSpot
            {
                Name = "Ekebysjön",
                Description = "Sjö",
                Latitude = 59.407450,
                Longitude = 18.056444,
                Depth = 3,
                HasRules = true,
                IsForbidden = true,
                IsDeleted = false,
                IsFishingCardRequired = false,
                Location = geometryFactory.CreatePoint(new Coordinate(18.056444, 59.407450))
            };
            db.FishingSpots.Add(Ekebysjön);
        }
        var Fatburen = db.FishingSpots.FirstOrDefault(s => s.Name == "Fatburen");
        if (Fatburen is null)
        {
            Fatburen = new FishingSpot
            {
                Name = "Fatburen",
                Description = "Sjö",
                Latitude = 59.233003,
                Longitude = 18.292093,
                Depth = 4,
                HasRules = true,
                IsForbidden = false,
                IsDeleted = false,
                IsFishingCardRequired = true,
                Location = geometryFactory.CreatePoint(new Coordinate(18.292093, 59.233003))
            };
            db.FishingSpots.Add(Fatburen);
        }
        var Flaten = db.FishingSpots.FirstOrDefault(s => s.Name == "Flaten");
        if (Flaten is null)
        {
            Flaten = new FishingSpot
            {
                Name = "Flaten",
                Description = "Sjö",
                Latitude = 59.263290,
                Longitude = 18.167828,
                Depth = 12,
                HasRules = true,
                IsForbidden = false,
                IsDeleted = false,
                IsFishingCardRequired = true,
                Location = geometryFactory.CreatePoint(new Coordinate(18.167828, 59.263290))
            };
            db.FishingSpots.Add(Flaten);
        }

        var Gömmaren = db.FishingSpots.FirstOrDefault(s => s.Name == "Gömmaren");
        if (Gömmaren is null)
        {
            Gömmaren = new FishingSpot
            {
                Name = "Gömmaren",
                Description = "Sjö",
                Latitude = 59.252924,
                Longitude = 17.918516,
                Depth = 5.6,
                HasRules = true,
                IsForbidden = false,
                IsDeleted = false,
                IsFishingCardRequired = true,
                Location = geometryFactory.CreatePoint(new Coordinate(17.918516, 59.252924))
            };
            db.FishingSpots.Add(Gömmaren);
        }
        var Hammarbysjön = db.FishingSpots.FirstOrDefault(s => s.Name == "Hammarbysjön");
        if (Hammarbysjön is null)
        {
            Hammarbysjön = new FishingSpot
            {
                Name = "Hammarbysjön",
                Description = "Sjö",
                Latitude = 59.307251,
                Longitude = 18.100494,
                Depth = 15,
                HasRules = true,
                IsForbidden = false,
                IsDeleted = false,
                IsFishingCardRequired = false,
                Location = geometryFactory.CreatePoint(new Coordinate(18.100494, 59.307251))
            };
            db.FishingSpots.Add(Hammarbysjön);
        }
        var Husarviken = db.FishingSpots.FirstOrDefault(s => s.Name == "Husarviken");
        if (Husarviken is null)
        {
            Husarviken = new FishingSpot
            {
                Name = "Husarviken",
                Description = "Sjö",
                Latitude = 59.360762,
                Longitude = 18.094076,
                Depth = 5,
                HasRules = true,
                IsForbidden = false,
                IsDeleted = false,
                IsFishingCardRequired = true,
                Location = geometryFactory.CreatePoint(new Coordinate(18.094976, 59.360762))
            };
            db.FishingSpots.Add(Husarviken);
        }
        var Igelbäcken = db.FishingSpots.FirstOrDefault(s => s.Name == "Igelbäcken");
        if (Igelbäcken is null)
        {
            Igelbäcken = new FishingSpot
            {
                Name = "Igelbäcken",
                Description = "Å",
                Latitude = 59.387899,
                Longitude = 17.983430,
                Depth = 1,
                HasRules = false,
                IsForbidden = true,
                IsDeleted = false,
                IsFishingCardRequired = false,
                Location = geometryFactory.CreatePoint(new Coordinate(17.983430, 59.387899))
            };
            db.FishingSpots.Add(Igelbäcken);
        }
        var Isbladskärret = db.FishingSpots.FirstOrDefault(s => s.Name == "Isbladskärret");
        if (Isbladskärret is null)
        {
            Isbladskärret = new FishingSpot
            {
                Name = "Isbladskärret",
                Description = "Sjö",
                Latitude = 59.325995,
                Longitude = 18.142779,
                Depth = 1,
                HasRules = false,
                IsForbidden = true,
                IsDeleted = false,
                IsFishingCardRequired = false,
                Location = geometryFactory.CreatePoint(new Coordinate(18.142779, 59.325995))
            };
            db.FishingSpots.Add(Isbladskärret);
        }
        var Judarn = db.FishingSpots.FirstOrDefault(s => s.Name == "Judarn");
        if (Judarn is null)
        {
            Judarn = new FishingSpot
            {
                Name = "Judarn",
                Description = "Sjö",
                Latitude = 59.337575,
                Longitude = 17.915600,
                Depth = 8,
                HasRules = true,
                IsForbidden = false,
                IsDeleted = false,
                IsFishingCardRequired = true,
                Location = geometryFactory.CreatePoint(new Coordinate(17.915600, 59.337575))
            };
            db.FishingSpots.Add(Judarn);
        }
        var Järlasjön = db.FishingSpots.FirstOrDefault(s => s.Name == "Järlasjön");
        if (Järlasjön is null)
        {
            Järlasjön = new FishingSpot
            {
                Name = "Järlasjön",
                Description = "Sjö",
                Latitude = 59.301840,
                Longitude = 18.157532,
                Depth = 22,
                HasRules = true,
                IsForbidden = false,
                IsDeleted = false,
                IsFishingCardRequired = true,
                Location = geometryFactory.CreatePoint(new Coordinate(18.157532, 59.301840))
            };
            db.FishingSpots.Add(Järlasjön);
        }
        var Karlbergkanalen = db.FishingSpots.FirstOrDefault(s => s.Name == "Karlbergskanalen");
        if (Karlbergkanalen is null)
        {
            Karlbergkanalen = new FishingSpot
            {
                Name = "Karlbergskanalen",
                Description = "Kanal",
                Latitude = 59.340159,
                Longitude = 18.015739,
                Depth = 4.5,
                HasRules = true,
                IsForbidden = false,
                IsDeleted = false,
                IsFishingCardRequired = true,
                Location = geometryFactory.CreatePoint(new Coordinate(18.015739, 59.340159))
            };
            db.FishingSpots.Add(Karlbergkanalen);
        }
        var Klarasjön = db.FishingSpots.FirstOrDefault(s => s.Name == "Klarasjön");
        if (Klarasjön is null)
        {
            Klarasjön = new FishingSpot
            {
                Name = "Klarasjön",
                Description = "Sjö",
                Latitude = 59.339787,
                Longitude = 18.022234,
                Depth = 4.5,
                HasRules = true,
                IsForbidden = false,
                IsDeleted = false,
                IsFishingCardRequired = true,
                Location = geometryFactory.CreatePoint(new Coordinate(18.022234, 59.339787))
            };
            db.FishingSpots.Add(Klarasjön);
        }
        var Kyrksjön = db.FishingSpots.FirstOrDefault(s => s.Name == "Kyrksjön");
        if (Kyrksjön is null)
        {
            Kyrksjön = new FishingSpot
            {
                Name = "Kyrksjön",
                Description = "Sjö",
                Latitude = 59.349487,
                Longitude = 17.916329,
                Depth = 4.5,
                HasRules = true,
                IsForbidden = false,
                IsDeleted = false,
                IsFishingCardRequired = true,
                Location = geometryFactory.CreatePoint(new Coordinate(17.916329, 59.349487))
            };
            db.FishingSpots.Add(Kyrksjön);
        }
        var Källtorpssjön = db.FishingSpots.FirstOrDefault(s => s.Name == "Källtorpssjön");
        if (Källtorpssjön is null)
        {
            Källtorpssjön = new FishingSpot
            {
                Name = "Källtorpssjön",
                Description = "Sjö",
                Latitude = 59.290193,
                Longitude = 18.170891,
                Depth = 7.6,
                HasRules = true,
                IsForbidden = false,
                IsDeleted = false,
                IsFishingCardRequired = true,
                Location = geometryFactory.CreatePoint(new Coordinate(18.170891, 59.290193))
            };
            db.FishingSpots.Add(Källtorpssjön);
        }
        var Laduviken = db.FishingSpots.FirstOrDefault(s => s.Name == "Laduviken");
        if (Laduviken is null)
        {
            Laduviken = new FishingSpot
            {
                Name = "Laduviken",
                Description = "Sjö",
                Latitude = 59.360582,
                Longitude = 18.077092,
                Depth = 3.2,
                HasRules = false,
                IsForbidden = false,
                IsDeleted = false,
                IsFishingCardRequired = false,
                Location = geometryFactory.CreatePoint(new Coordinate(18.077092, 59.360582))
            };
            db.FishingSpots.Add(Laduviken);
        }
        var Lappkärret = db.FishingSpots.FirstOrDefault(s => s.Name == "Lappkärret");
        if (Lappkärret is null)
        {
            Lappkärret = new FishingSpot
            {
                Name = "Lappkärret",
                Description = "Sjö",
                Latitude = 59.368524,
                Longitude = 18.068953,
                Depth = 2,
                HasRules = true,
                IsForbidden = false,
                IsDeleted = false,
                IsFishingCardRequired = true,
                Location = geometryFactory.CreatePoint(new Coordinate(18.068953, 59.368524))
            };
            db.FishingSpots.Add(Lappkärret);
        }
        var LillaVärtan = db.FishingSpots.FirstOrDefault(s => s.Name == "Lilla Värtan");
        if (LillaVärtan is null)
        {
            LillaVärtan = new FishingSpot
            {
                Name = "Lilla Värtan",
                Description = "Sjö",
                Latitude = 59.339592,
                Longitude = 18.149918,
                Depth = 45,
                HasRules = true,
                IsForbidden = false,
                IsDeleted = false,
                IsFishingCardRequired = false,
                Location = geometryFactory.CreatePoint(new Coordinate(18.149918, 59.339592))
            };
            db.FishingSpots.Add(LillaVärtan);
        }
        var Lillsjön = db.FishingSpots.FirstOrDefault(s => s.Name == "Lillsjön");
        if (Lillsjön is null)
        {
            Lillsjön = new FishingSpot
            {
                Name = "Lillsjön",
                Description = "Sjö",
                Latitude = 59.361884,
                Longitude = 18.085228,
                Depth = 8.6,
                HasRules = true,
                IsForbidden = false,
                IsDeleted = false,
                IsFishingCardRequired = true,
                Location = geometryFactory.CreatePoint(new Coordinate(18.085228, 59.361884))
            };
            db.FishingSpots.Add(Lillsjön);
        }
        var Långsjön_Hanveden = db.FishingSpots.FirstOrDefault(s => s.Name == "Långsjön");
        if (Långsjön_Hanveden is null)
        {
            Långsjön_Hanveden = new FishingSpot
            {
                Name = "Långsjön",
                Description = "Sjö",
                Latitude = 59.195675,
                Longitude = 18.293351,
                Depth = 7,
                HasRules = true,
                IsForbidden = false,
                IsDeleted = false,
                IsFishingCardRequired = true,
                Location = geometryFactory.CreatePoint(new Coordinate(18.293351, 59.195675))
            };
            db.FishingSpots.Add(Långsjön_Hanveden);
        }
        var Långsjön_Älvsjö = db.FishingSpots.FirstOrDefault(s => s.Name == "Långsjön");
        if (Långsjön_Älvsjö is null)
        {
            Långsjön_Älvsjö = new FishingSpot
            {
                Name = "Långsjön",
                Description = "Sjö",
                Latitude = 59.266677,
                Longitude = 17.968806,
                Depth = 6.8,
                HasRules = true,
                IsForbidden = false,
                IsDeleted = false,
                IsFishingCardRequired = true,
                Location = geometryFactory.CreatePoint(new Coordinate(17.968806, 59.266677))
            };
            db.FishingSpots.Add(Långsjön_Älvsjö);
        }
        var Magelungen = db.FishingSpots.FirstOrDefault(s => s.Name == "Magelungen");
        if (Magelungen is null)
        {
            Magelungen = new FishingSpot
            {
                Name = "Magelungen",
                Description = "Sjö",
                Latitude = 59.227580,
                Longitude = 18.103098,
                Depth = 13.7,
                HasRules = true,
                IsForbidden = false,
                IsDeleted = false,
                IsFishingCardRequired = true,
                Location = geometryFactory.CreatePoint(new Coordinate(18.103098, 59.227580))
            };
            db.FishingSpots.Add(Magelungen);
        }
        var Mörtsjön = db.FishingSpots.FirstOrDefault(s => s.Name == "Mörtsjön");
        if (Mörtsjön is null)
        {
            Mörtsjön = new FishingSpot
            {
                Name = "Mörtsjön",
                Description = "Sjö",
                Latitude = 59.450994,
                Longitude = 18.016555,
                Depth = 13.1,
                HasRules = true,
                IsForbidden = false,
                IsDeleted = false,
                IsFishingCardRequired = true,
                Location = geometryFactory.CreatePoint(new Coordinate(18.016555, 59.450994))
            };
            db.FishingSpots.Add(Mörtsjön);
        }
        var Nacka_Ström = db.FishingSpots.FirstOrDefault(s => s.Name == "Nacka Ström");
        if (Nacka_Ström is null)
        {
            Nacka_Ström = new FishingSpot
            {
                Name = "Nacka Ström",
                Description = "Å",
                Latitude = 59.300070,
                Longitude = 18.152438,
                Depth = 19,
                HasRules = true,
                IsForbidden = false,
                IsDeleted = false,
                IsFishingCardRequired = true,
                Location = geometryFactory.CreatePoint(new Coordinate(18.152438, 59.300070))
            };
            db.FishingSpots.Add(Nacka_Ström);
        }
        var Orlången = db.FishingSpots.FirstOrDefault(s => s.Name == "Orlången");
        if (Orlången is null)
        {
            Orlången = new FishingSpot
            {
                Name = "Orlången",
                Description = "Sjö",
                Latitude = 59.197004,
                Longitude = 18.040926,
                Depth = 10.2,
                HasRules = true,
                IsForbidden = false,
                IsDeleted = false,
                IsFishingCardRequired = true,
                Location = geometryFactory.CreatePoint(new Coordinate(18.040926, 59.197004))
            };
            db.FishingSpots.Add(Orlången);
        }
        var Riddarfjärden = db.FishingSpots.FirstOrDefault(s => s.Name == "Riddarfjärden");
        if (Riddarfjärden is null)
        {
            Riddarfjärden = new FishingSpot
            {
                Name = "Riddarfjärden",
                Description = "Sjö",
                Latitude = 59.323222,
                Longitude = 18.060636,
                Depth = 23,
                HasRules = true,
                IsForbidden = false,
                IsDeleted = false,
                IsFishingCardRequired = false,
                Location = geometryFactory.CreatePoint(new Coordinate(18.060636, 59.323222))
            };
            db.FishingSpots.Add(Riddarfjärden);
        }
        var Råcksta_Träsk = db.FishingSpots.FirstOrDefault(s => s.Name == "Råcksta Träsk");
        if (Råcksta_Träsk is null)
        {
            Råcksta_Träsk = new FishingSpot
            {
                Name = "Råcksta Träsk",
                Description = "Sjö",
                Latitude = 59.352725,
                Longitude = 17.876182,
                Depth = 2.3,
                HasRules = false,
                IsForbidden = false,
                IsDeleted = false,
                IsFishingCardRequired = true,
                Location = geometryFactory.CreatePoint(new Coordinate(17.876182, 59.352725))
            };
            db.FishingSpots.Add(Råcksta_Träsk);
        }
        var Rönningesjön = db.FishingSpots.FirstOrDefault(s => s.Name == "Rönningesjön");
        if (Rönningesjön is null)
        {
            Rönningesjön = new FishingSpot
            {
                Name = "Rönningesjön",
                Description = "Sjö",
                Latitude = 59.456977,
                Longitude = 18.107062,
                Depth = 4.7,
                HasRules = true,
                IsForbidden = false,
                IsDeleted = false,
                IsFishingCardRequired = true,
                Location = geometryFactory.CreatePoint(new Coordinate(18.107062, 59.456977))
            };
            db.FishingSpots.Add(Rönningesjön);
        }
        var Rösjön = db.FishingSpots.FirstOrDefault(s => s.Name == "Rösjön");
        if (Rösjön is null)
        {
            Rösjön = new FishingSpot
            {
                Name = "Rösjön",
                Description = "Sjö",
                Latitude = 59.442871,
                Longitude = 17.996758,
                Depth = 7.3,
                HasRules = true,
                IsForbidden = false,
                IsDeleted = false,
                IsFishingCardRequired = true,
                Location = geometryFactory.CreatePoint(new Coordinate(17.996758, 59.442871))
            };
            db.FishingSpots.Add(Rösjön);
        }
        var Saltsjön = db.FishingSpots.FirstOrDefault(s => s.Name == "Saltsjön");
        if (Saltsjön is null)
        {
            Saltsjön = new FishingSpot
            {
                Name = "Saltsjön",
                Description = "Sjö",
                Latitude = 59.323987,
                Longitude = 18.188551,
                Depth = 36,
                HasRules = true,
                IsForbidden = false,
                IsDeleted = false,
                IsFishingCardRequired = false,
                Location = geometryFactory.CreatePoint(new Coordinate(18.188551, 59.323987))
            };
            db.FishingSpots.Add(Saltsjön);
        }
        var Sicklasjön = db.FishingSpots.FirstOrDefault(s => s.Name == "Sicklasjön");
        if (Sicklasjön is null)
        {
            Sicklasjön = new FishingSpot
            {
                Name = "Sicklasjön",
                Description = "Sjö",
                Latitude = 59.301374,
                Longitude = 18.133793,
                Depth = 5.6,
                HasRules = false,
                IsForbidden = false,
                IsDeleted = false,
                IsFishingCardRequired = true,
                Location = geometryFactory.CreatePoint(new Coordinate(18.133793, 59.301374))
            };
            db.FishingSpots.Add(Sicklasjön);
        }
        var Spegeldammen = db.FishingSpots.FirstOrDefault(s => s.Name == "Spegeldammen");
        if (Spegeldammen is null)
        {
            Spegeldammen = new FishingSpot
            {
                Name = "Spegeldammen",
                Description = "Sjö",
                Latitude = 59.365440,
                Longitude = 18.078189,
                Depth = 0,
                HasRules = true,
                IsForbidden = true,
                IsDeleted = false,
                IsFishingCardRequired = true,
                Location = geometryFactory.CreatePoint(new Coordinate(18.078189, 59.365440))
            };
            db.FishingSpots.Add(Spegeldammen);
        }
        var Svindersviken = db.FishingSpots.FirstOrDefault(s => s.Name == "Svindersviken");
        if (Svindersviken is null)
        {
            Svindersviken = new FishingSpot
            {
                Name = "Svindersviken",
                Description = "Sjö",
                Latitude = 59.313965,
                Longitude = 18.139238,
                Depth = 30,
                HasRules = true,
                IsForbidden = false,
                IsDeleted = false,
                IsFishingCardRequired = false,
                Location = geometryFactory.CreatePoint(new Coordinate(18.139238, 59.313965))
            };
            db.FishingSpots.Add(Svindersviken);
        }
        var Säbysjön = db.FishingSpots.FirstOrDefault(s => s.Name == "Säbysjön");
        if (Säbysjön is null)
        {
            Säbysjön = new FishingSpot
            {
                Name = "Säbysjön",
                Description = "Sjö",
                Latitude = 59.43517,
                Longitude = 17.86902,
                Depth = 6,
                HasRules = true,
                IsForbidden = true,
                IsDeleted = false,
                IsFishingCardRequired = true,
                Location = geometryFactory.CreatePoint(new Coordinate(17.86902, 59.43517))
            };
            db.FishingSpots.Add(Säbysjön);
        }
        var Sätraån = db.FishingSpots.FirstOrDefault(s => s.Name == "Sätraån");
        if (Sätraån is null)
        {
            Sätraån = new FishingSpot
            {
                Name = "Sätraån",
                Description = "Å",
                Latitude = 59.285875,
                Longitude = 17.899050,
                Depth = 5.6,
                HasRules = true,
                IsForbidden = false,
                IsDeleted = false,
                IsFishingCardRequired = false,
                Location = geometryFactory.CreatePoint(new Coordinate(17.899050, 59.285875))
            };
            db.FishingSpots.Add(Sätraån);
        }
        var Söderbysjön = db.FishingSpots.FirstOrDefault(s => s.Name == "Söderbysjön");
        if (Söderbysjön is null)
        {
            Söderbysjön = new FishingSpot
            {
                Name = "Söderbysjön",
                Description = "Sjö",
                Latitude = 59.282024,
                Longitude = 18.151072,
                Depth = 5.5,
                HasRules = true,
                IsForbidden = false,
                IsDeleted = false,
                IsFishingCardRequired = true,
                Location = geometryFactory.CreatePoint(new Coordinate(18.151072, 59.282024))
            };
            db.FishingSpots.Add(Söderbysjön);
        }
        var Trekanten = db.FishingSpots.FirstOrDefault(s => s.Name == "Trekanten");
        if (Trekanten is null)
        {
            Trekanten = new FishingSpot
            {
                Name = "Trekanten",
                Description = "Sjö",
                Latitude = 59.311683,
                Longitude = 18.018045,
                Depth = 6.6,
                HasRules = true,
                IsForbidden = false,
                IsDeleted = false,
                IsFishingCardRequired = true,
                Location = geometryFactory.CreatePoint(new Coordinate(18.018045, 59.311683))
            };
            db.FishingSpots.Add(Trekanten);
        }
        var Trehörningen = db.FishingSpots.FirstOrDefault(s => s.Name == "Trehörningen");
        if (Trehörningen is null)
        {
            Trehörningen = new FishingSpot
            {
                Name = "Trehörningen",
                Description = "Sjö",
                Latitude = 59.233657,
                Longitude = 18.026415,
                Depth = 6.8,
                HasRules = true,
                IsForbidden = false,
                IsDeleted = false,
                IsFishingCardRequired = true,
                Location = geometryFactory.CreatePoint(new Coordinate(18.233657, 59.233657))
            };
            db.FishingSpots.Add(Trehörningen);
        }
        var Tullan = db.FishingSpots.FirstOrDefault(s => s.Name == "Tullan");
        if (Tullan is null)
        {
            Tullan = new FishingSpot
            {
                Name = "Tullan",
                Description = "Sjö",
                Latitude = 59.213342,
                Longitude = 17.693496,
                Depth = 11,
                HasRules = false,
                IsForbidden = true,
                IsDeleted = false,
                IsFishingCardRequired = false,
                Location = geometryFactory.CreatePoint(new Coordinate(17.693496, 59.213342))
            };
            db.FishingSpots.Add(Tullan);
        }
        var Tullingesjön = db.FishingSpots.FirstOrDefault(s => s.Name == "Tullingesjön");
        if (Tullingesjön is null)
        {
            Tullingesjön = new FishingSpot
            {
                Name = "Tullingesjön",
                Description = "Sjö",
                Latitude = 59.216139,
                Longitude = 17.873941,
                Depth = 30,
                HasRules = false,
                IsForbidden = false,
                IsDeleted = false,
                IsFishingCardRequired = true,
                Location = geometryFactory.CreatePoint(new Coordinate(17.873941, 59.216139))
            };
            db.FishingSpots.Add(Tullingesjön);
        }
        var Uggleviken = db.FishingSpots.FirstOrDefault(s => s.Name == "Uggleviken");
        if (Uggleviken is null)
        {
            Uggleviken = new FishingSpot
            {
                Name = "Uggleviken",
                Description = "Sjö",
                Latitude = 59.355032,
                Longitude = 18.075253,
                Depth = 0,
                HasRules = false,
                IsForbidden = true,
                IsDeleted = false,
                IsFishingCardRequired = false,
                Location = geometryFactory.CreatePoint(new Coordinate(18.075253, 59.355032))
            };
            db.FishingSpots.Add(Uggleviken);
        }
        var Ullnassjön = db.FishingSpots.FirstOrDefault(s => s.Name == "Ullnassjön");
        if (Ullnassjön is null)
        {
            Ullnassjön = new FishingSpot
            {
                Name = "Ullnassjön",
                Description = "Sjö",
                Latitude = 59.485933,
                Longitude = 18.154253,
                Depth = 6.2,
                HasRules = true,
                IsForbidden = false,
                IsDeleted = false,
                IsFishingCardRequired = true,
                Location = geometryFactory.CreatePoint(new Coordinate(18.154253, 59.485933))
            };
            db.FishingSpots.Add(Ullnassjön);
        }
        var Ulvsjön = db.FishingSpots.FirstOrDefault(s => s.Name == "Ulvsjön");
        if (Ulvsjön is null)
        {
            Ulvsjön = new FishingSpot
            {
                Name = "Ulvsjön",
                Description = "Sjö",
                Latitude = 59.276656,
                Longitude = 18.164705,
                Depth = 3.7,
                HasRules = false,
                IsForbidden = false,
                IsDeleted = false,
                IsFishingCardRequired = true,
                Location = geometryFactory.CreatePoint(new Coordinate(18.164705, 59.276656))
            };
            db.FishingSpots.Add(Ulvsjön);
        }
        var Ulvsundasjön = db.FishingSpots.FirstOrDefault(s => s.Name == "Ulvsundasjön");
        if (Ulvsundasjön is null)
        {
            Ulvsundasjön = new FishingSpot
            {
                Name = "Ulvsundasjön",
                Description = "Sjö",
                Latitude = 59.338078,
                Longitude = 17.996701,
                Depth = 20,
                HasRules = true,
                IsForbidden = false,
                IsDeleted = false,
                IsFishingCardRequired = false,
                Location = geometryFactory.CreatePoint(new Coordinate(17.996701, 59.338078))
            };
            db.FishingSpots.Add(Ulvsundasjön);
        }
        var Uttran = db.FishingSpots.FirstOrDefault(s => s.Name == "Uttran");
        if (Uttran is null)
        {
            Uttran = new FishingSpot
            {
                Name = "Uttran",
                Description = "Sjö",
                Latitude = 59.195442,
                Longitude = 17.799252,
                Depth = 16,
                HasRules = true,
                IsForbidden = false,
                IsDeleted = false,
                IsFishingCardRequired = true,
                Location = geometryFactory.CreatePoint(new Coordinate(17.799252, 59.195442))
            };
            db.FishingSpots.Add(Uttran);
        }
        var Vallentunasjön = db.FishingSpots.FirstOrDefault(s => s.Name == "Vallentunasjön");
        if (Vallentunasjön is null)
        {
            Vallentunasjön = new FishingSpot
            {
                Name = "Vallentunasjön",
                Description = "Sjö",
                Latitude = 59.524161,
                Longitude = 18.057875,
                Depth = 5,
                HasRules = true,
                IsForbidden = false,
                IsDeleted = false,
                IsFishingCardRequired = true,
                Location = geometryFactory.CreatePoint(new Coordinate(18.057875, 59.524161))
            };
            db.FishingSpots.Add(Vallentunasjön);
        }
        var Vinterviken = db.FishingSpots.FirstOrDefault(s => s.Name == "Vinterviken");
        if (Vinterviken is null)
        {
            Vinterviken = new FishingSpot
            {
                Name = "Vinterviken",
                Description = "Sjö",
                Latitude = 59.311183,
                Longitude = 17.986128,
                Depth = 15,
                HasRules = false,
                IsForbidden = false,
                IsDeleted = false,
                IsFishingCardRequired = false,
                Location = geometryFactory.CreatePoint(new Coordinate(17.986128, 59.311183))
            };
            db.FishingSpots.Add(Vinterviken);
        }
        var Ådran = db.FishingSpots.FirstOrDefault(s => s.Name == "Ådran");
        if (Ådran is null)
        {
            Ådran = new FishingSpot
            {
                Name = "Ådran",
                Description = "Sjö",
                Latitude = 59.160355,
                Longitude = 18.017245,
                Depth = 0,
                HasRules = false,
                IsForbidden = false,
                IsDeleted = false,
                IsFishingCardRequired = true,
                Location = geometryFactory.CreatePoint(new Coordinate(18.017245, 59.160355))
            };
            db.FishingSpots.Add(Ådran);
        }
        var Årstaviken = db.FishingSpots.FirstOrDefault(s => s.Name == "Årstaviken");
        if (Årstaviken is null)
        {
            Årstaviken = new FishingSpot
            {
                Name = "Årstaviken",
                Description = "Sjö",
                Latitude = 59.304227,
                Longitude = 18.059547,
                Depth = 9,
                HasRules = false,
                IsForbidden = false,
                IsDeleted = false,
                IsFishingCardRequired = false,
                Location = geometryFactory.CreatePoint(new Coordinate(18.059547, 59.304227))
            };
            db.FishingSpots.Add(Årstaviken);
        }
        var Ältasjön = db.FishingSpots.FirstOrDefault(s => s.Name == "Ältasjön");
        if (Ältasjön is null)
        {
            Ältasjön = new FishingSpot
            {
                Name = "Ältasjön",
                Description = "Sjö",
                Latitude = 59.262745,
                Longitude = 18.170782,
                Depth = 10.2,
                HasRules = true,
                IsForbidden = false,
                IsDeleted = false,
                IsFishingCardRequired = true,
                Location = geometryFactory.CreatePoint(new Coordinate(18.170782, 59.262745))
            };
            db.FishingSpots.Add(Ältasjön);
        }
        var Ösbysjön = db.FishingSpots.FirstOrDefault(s => s.Name == "Ösbysjön");
        if (Ösbysjön is null)
        {
            Ösbysjön = new FishingSpot
            {
                Name = "Ösbysjön",
                Description = "Sjö",
                Latitude = 59.402248,
                Longitude = 18.062708,
                Depth = 0,
                HasRules = false,
                IsForbidden = true,
                IsDeleted = false,
                IsFishingCardRequired = false,
                Location = geometryFactory.CreatePoint(new Coordinate(18.062708, 59.402248))
            };
            db.FishingSpots.Add(Ösbysjön);
        }
        var Östra_Mälaren = db.FishingSpots.FirstOrDefault(s => s.Name == "Östra Mälaren");
        if (Östra_Mälaren is null)
        {
            Östra_Mälaren = new FishingSpot
            {
                Name = "Östra Mälaren",
                Description = "Sjö",
                Latitude = 59.369374,
                Longitude = 18.275169,
                Depth = 60,
                HasRules = true,
                IsForbidden = false,
                IsDeleted = false,
                IsFishingCardRequired = false,
                Location = geometryFactory.CreatePoint(new Coordinate(18.275169, 59.369374))
            };
            db.FishingSpots.Add(Östra_Mälaren);
        }
        var Översjön = db.FishingSpots.FirstOrDefault(s => s.Name == "Översjön");
        if (Översjön is null)
        {
            Översjön = new FishingSpot
            {
                Name = "Översjön",
                Description = "Sjö",
                Latitude = 59.454587,
                Longitude = 17.846399,
                Depth = 4.1,
                HasRules = true,
                IsForbidden = false,
                IsDeleted = false,
                IsFishingCardRequired = true,
                Location = geometryFactory.CreatePoint(new Coordinate(17.846399, 59.454587))
            };
            db.FishingSpots.Add(Översjön);
        }

        db.SaveChanges();

        // Fishing Lures
        if (!db.FishingLures.Any())
        {
            var spinner = new FishingLure { Name = "Spinner", Type = "Spinnerbait", Description = "En spinner är ett klassiskt fiskedrag som består av ett roterande metallblad som skapar starka vibrationer och reflexer i vattnet. När bladet snurrar lockas rovfiskar genom både ljud, rörelse och ljusreflektioner. Spinners används ofta vid fiske efter abborre, gädda, öring och ibland även lax. Draget fungerar mycket bra i både sjöar, älvar och mindre vattendrag eftersom det är enkelt att använda och effektivt även för nybörjare. Spinners känns igen på sitt snurrande blad och sin kompakta metallkropp.", ImageUrl = $"{fishluresUrl}/1"};
            var jig = new FishingLure { Name = "Jig", Type = "Jig", Description = "En jig är ett mycket populärt fiskedrag som vanligtvis består av ett mjukt gummibete monterat på ett jigghuvud av bly eller metall. Betet är skapat för att röra sig naturligt i vattnet och imitera småfisk eller andra vattenlevande djur. Jiggar används främst vid bottennära fiske efter gös, abborre och gädda. Fiskaren kan variera invevningen för att skapa hoppande eller glidande rörelser som triggar rovfisken till hugg. Jiggar har ofta en mjuk kropp och ett runt metallhuvud med krok.", ImageUrl = $"{fishluresUrl}/3" };
            var jerkbait = new FishingLure { Name = "Jerkbait", Type = "Jerkbait", Description = "Ett jerkbait är ett större hårdbete som fiskas med ryckiga rörelser för att imitera en stressad eller flyende bytesfisk. Tekniken innebär att fiskaren gör korta drag med spöet samtidigt som betet får glida genom vattnet i sidled. Jerkbaits används främst vid gäddfiske eftersom de ofta lockar fram aggressiva attacker från stora rovfiskar. Många jerkbaits är ganska tunga och kräver kraftigare utrustning än vanliga drag. Ett jerkbait är vanligtvis längre och smalare än många andra hårdbeten.", ImageUrl = $"{fishluresUrl}/4" };
            var tail = new FishingLure { Name = "Mjukbete (Twister Tail)", Type = "Soft plastic", Description = "Mjukbeten med twister tail är gummibeten med en böjd svans som skapar livliga vibrationer och rörelser i vattnet. Svansen börjar röra sig redan vid låg fart och lockar rovfisk genom sitt naturliga utseende. Dessa beten används ofta tillsammans med jigghuvuden och är populära vid fiske efter abborre, gös och gädda. De är särskilt effektiva i kallt vatten där fiskarna reagerar på långsamma och mjuka rörelser. Twister tails känns igen på sin spiralformade eller böjda svans.", ImageUrl = $"{fishluresUrl}/5" };
            var spoon = new FishingLure { Name = "Sked", Type = "Spoon", Description = "En sked är ett metallbete som rör sig wobblande genom vattnet och reflekterar ljus från sina blanka ytor. Rörelsen efterliknar en skadad småfisk och väcker rovfiskars jaktinstinkt. Skeddrag används ofta vid fiske efter gädda, lax, öring och röding. De fungerar bra både vid kastfiske och trolling eftersom de kan fiskas på flera olika djup. Skeddrag har vanligtvis en böjd metallform som påminner om en liten sked.", ImageUrl = $"{fishluresUrl}/6" };
            var fly = new FishingLure { Name = "Flugbete", Type = "Fly", Description = "Flugbeten är konstgjorda imitationer av insekter, larver eller småfisk som används inom flugfiske. De tillverkas ofta av fjädrar, tråd, päls och syntetiska material för att skapa ett naturtroget utseende. Flugor används främst för att fånga öring, harr, lax och regnbåge i strömmande vatten och sjöar. Det finns många olika typer av flugor beroende på vilken insekt eller vilket bytesdjur man vill efterlikna. Flugbeten är ofta små och mycket detaljerade jämfört med andra fiskedrag.", ImageUrl = $"{fishluresUrl}/7" };
            var worm = new FishingLure { Name = "Maskbete", Type = "Soft plastic", Description = "Maskbeten är mjuka gummibeten som efterliknar maskar, larver eller andra små djur som fiskar äter naturligt. De används ofta vid abborr- och bassfiske eftersom deras långsamma och naturliga rörelser lockar försiktiga fiskar. Betena kan fiskas på många olika sätt, exempelvis längs botten eller med långsam invevning genom växtlighet. Många sportfiskare uppskattar maskbeten eftersom de är mycket mångsidiga och fungerar året runt. Ett maskbete är vanligtvis långt, smalt och mycket flexibelt.", ImageUrl = $"{fishluresUrl}/8" };
            var crankbait = new FishingLure { Name = "Crankbait", Type = "Crankbait", Description = "Crankbaits är hårdbeten med en sked framtill som gör att de dyker och vibrerar kraftigt under invevning. Betet är designat för att imitera småfisk som simmar snabbt genom vattnet. Crankbaits används ofta vid fiske efter abborre, gös, gädda och bass. Olika modeller är anpassade för olika djup, från grunt vatten till flera meter under ytan. Crankbaits är ofta kortare och rundare än vanliga wobblers.", ImageUrl = $"{fishluresUrl}/9" };
            var popper = new FishingLure { Name = "Popper", Type = "Topwater", Description = "En popper är ett ytbete som används för fiske precis på vattenytan. Betet har en konkav front som skapar plaskande ljud och bubblor när det rycks fram genom vattnet. Dessa ljud och rörelser lockar rovfiskar att attackera explosivt från under ytan. Poppers används ofta vid fiske efter gädda, abborre och bass under varma sommardagar. Det som gör en popper unik är dess stora öppna mun framtill.", ImageUrl = $"{fishluresUrl}/10" };
            var streamer = new FishingLure { Name = "Streamer", Type = "Fly", Description = "En streamer är en större typ av fluga som är designad för att imitera småfisk eller större vattenlevande djur. Den används inom flugfiske efter rovfisk som öring, lax och gädda. Streamers fiskas ofta med varierad hastighet för att skapa en livlig och naturlig rörelse i vattnet. Många streamers är färgglada eller glittrande för att synas tydligt även i mörkt eller grumligt vatten. Streamers är vanligtvis längre och fluffigare än vanliga torrflugor.", ImageUrl = $"{fishluresUrl}/11" };
            var nymph = new FishingLure { Name = "Nymf", Type = "Fly", Description = "En nymf är ett flugbete som efterliknar insektslarver och andra små vattenlevande organismer under ytan. Eftersom många fiskar äter larver större delen av året är nymffiske ofta mycket effektivt. Nymfer används främst vid fiske efter öring och harr i strömmande vatten. Betena fiskas vanligtvis nära botten där riktiga larver naturligt befinner sig. Nymfer är ofta små, runda och designade för att likna undervattenslevande insekter.", ImageUrl = $"{fishluresUrl}/12" };

            void AddToLure(FishingLure lure, params FishSpecies?[] species)
            {
                foreach (var s in species)
                    if (s != null) lure.FishSpecies.Add(s);
            }


            var predators = new[] { gädda, gös, abborre, öring, havsöring, lax, lake, asp };

            var cyprinids = new[] { karp, sutare, mört, braxen, björkna, ruda, sarv, löja, gers, nissöga, sik, siklöja, sil, nors, skarpsill, strömming };

            var others = new[] { regnbåge, röding, harr, ål, gärs, kräfta };


            AddToLure(spinner, predators);
            AddToLure(jig, predators);
            AddToLure(jerkbait, gädda, gös, asp, lake);
            AddToLure(tail, predators);
            AddToLure(spoon, predators);
            AddToLure(fly, regnbåge, röding, öring, havsöring);
            AddToLure(worm, cyprinids);
            AddToLure(crankbait, predators);
            AddToLure(popper, gädda, abborre, asp);
            AddToLure(streamer, predators);
            AddToLure(nymph, predators);


            if (gärs != null) AddToLure(spinner, gärs);
            if (kräfta != null) AddToLure(jig, kräfta);
            if (ål != null) AddToLure(worm, ål);
            if (lake != null) AddToLure(jig, lake);
            if (harr != null) AddToLure(fly, harr);
            if (röding != null) AddToLure(spoon, röding);

            db.FishingLures.AddRange(spinner, jig, jerkbait, tail, spoon, fly, worm, crankbait, popper, streamer, nymph);
            db.SaveChanges();
        }

        // FishingSpeciesFishingSpots
        if (!db.FishingSpeciesFishingSpots.Any())
        {
            var relationships = new HashSet<(int speciesId, int spotId, FishSpeciesFrequency freq)>();

            void AddRel(int speciesId, int spotId, FishSpeciesFrequency freq)
                => relationships.Add((speciesId, spotId, freq));

            if (gädda != null && abborre != null && karp != null && aglasjon != null)
            {
                AddRel(gädda.Id, aglasjon.Id, FishSpeciesFrequency.High);
                AddRel(abborre.Id, aglasjon.Id, FishSpeciesFrequency.High);
                AddRel(karp.Id, aglasjon.Id, FishSpeciesFrequency.Medium);
            }

            if (abborre != null && gädda != null && mört != null && braxen != null && gös != null && havsöring != null && Brunnsviken != null)
            {
                AddRel(abborre.Id, Brunnsviken.Id, FishSpeciesFrequency.High);
                AddRel(gädda.Id, Brunnsviken.Id, FishSpeciesFrequency.Medium);
                AddRel(mört.Id, Brunnsviken.Id, FishSpeciesFrequency.Medium);
                AddRel(braxen.Id, Brunnsviken.Id, FishSpeciesFrequency.Medium);
                AddRel(gös.Id, Brunnsviken.Id, FishSpeciesFrequency.Low);
                AddRel(havsöring.Id, Brunnsviken.Id, FishSpeciesFrequency.Low);
            }

            if (abborre != null && gädda != null && mört != null && braxen != null && gös != null && sutare != null && Bällstaån != null)
            {
                AddRel(abborre.Id, Bällstaån.Id, FishSpeciesFrequency.High);
                AddRel(gädda.Id, Bällstaån.Id, FishSpeciesFrequency.Medium);
                AddRel(mört.Id, Bällstaån.Id, FishSpeciesFrequency.Medium);
                AddRel(braxen.Id, Bällstaån.Id, FishSpeciesFrequency.Low);
                AddRel(gös.Id, Bällstaån.Id, FishSpeciesFrequency.Medium);
                AddRel(sutare.Id, Bällstaån.Id, FishSpeciesFrequency.Low);
            }

            if (abborre != null && gädda != null && mört != null && braxen != null && strömming != null && sutare != null && skarpsill != null && havsöring != null && Djurgårdsbrunnsviken != null)
            {
                AddRel(abborre.Id, Djurgårdsbrunnsviken.Id, FishSpeciesFrequency.High);
                AddRel(gädda.Id, Djurgårdsbrunnsviken.Id, FishSpeciesFrequency.Medium);
                AddRel(braxen.Id, Djurgårdsbrunnsviken.Id, FishSpeciesFrequency.Medium);
                AddRel(sutare.Id, Djurgårdsbrunnsviken.Id, FishSpeciesFrequency.Low);
                AddRel(mört.Id, Djurgårdsbrunnsviken.Id, FishSpeciesFrequency.Low);
                AddRel(strömming.Id, Djurgårdsbrunnsviken.Id, FishSpeciesFrequency.Low);
                AddRel(skarpsill.Id, Djurgårdsbrunnsviken.Id, FishSpeciesFrequency.Low);
                AddRel(sil.Id, Djurgårdsbrunnsviken.Id, FishSpeciesFrequency.Low);
                AddRel(havsöring.Id, Djurgårdsbrunnsviken.Id, FishSpeciesFrequency.Low);
            }

            if (abborre != null && gädda != null && gös != null && Drevviken != null)
            {
                AddRel(abborre.Id, Drevviken.Id, FishSpeciesFrequency.Medium);
                AddRel(gädda.Id, Drevviken.Id, FishSpeciesFrequency.Medium);
                AddRel(gös.Id, Drevviken.Id, FishSpeciesFrequency.High);
            }

            if (abborre != null && gös != null && mört != null && gädda != null && sik != null && strömming != null && lake != null && skarpsill != null && öring != null && Edsviken != null)
            {
                AddRel(abborre.Id, Edsviken.Id, FishSpeciesFrequency.High);
                AddRel(gädda.Id, Edsviken.Id, FishSpeciesFrequency.Medium);
                AddRel(gös.Id, Edsviken.Id, FishSpeciesFrequency.High);
                AddRel(mört.Id, Edsviken.Id, FishSpeciesFrequency.Low);
                AddRel(sik.Id, Edsviken.Id, FishSpeciesFrequency.Low);
                AddRel(strömming.Id, Edsviken.Id, FishSpeciesFrequency.Low);
                AddRel(lake.Id, Edsviken.Id, FishSpeciesFrequency.Low);
                AddRel(skarpsill.Id, Edsviken.Id, FishSpeciesFrequency.Low);
                AddRel(sil.Id, Edsviken.Id, FishSpeciesFrequency.Low);
                AddRel(öring.Id, Edsviken.Id, FishSpeciesFrequency.Low);
            }

            if (abborre != null && regnbåge != null && Fatburen != null)
            {
                AddRel(abborre.Id, Fatburen.Id, FishSpeciesFrequency.High);
                AddRel(regnbåge.Id, Fatburen.Id, FishSpeciesFrequency.Low);
            }

            if (abborre != null && gädda != null && mört != null && braxen != null && sutare != null && gös != null && Flaten != null)
            {
                AddRel(abborre.Id, Flaten.Id, FishSpeciesFrequency.High);
                AddRel(gädda.Id, Flaten.Id, FishSpeciesFrequency.Medium);
                AddRel(mört.Id, Flaten.Id, FishSpeciesFrequency.Medium);
                AddRel(braxen.Id, Flaten.Id, FishSpeciesFrequency.Medium);
                AddRel(sutare.Id, Flaten.Id, FishSpeciesFrequency.Medium);
                AddRel(gös.Id, Flaten.Id, FishSpeciesFrequency.Low);
            }

            if (abborre != null && regnbåge != null && gädda != null && mört != null && sarv != null && Gömmaren != null)
            {
                AddRel(abborre.Id, Gömmaren.Id, FishSpeciesFrequency.High);
                AddRel(regnbåge.Id, Gömmaren.Id, FishSpeciesFrequency.High);
                AddRel(gädda.Id, Gömmaren.Id, FishSpeciesFrequency.Medium);
                AddRel(mört.Id, Gömmaren.Id, FishSpeciesFrequency.Medium);
                AddRel(sarv.Id, Gömmaren.Id, FishSpeciesFrequency.Low);
            }

            if (gös != null && abborre != null && gädda != null && lax != null && strömming != null && öring != null && Hammarbysjön != null)
            {
                AddRel(gös.Id, Hammarbysjön.Id, FishSpeciesFrequency.High);
                AddRel(abborre.Id, Hammarbysjön.Id, FishSpeciesFrequency.High);
                AddRel(gädda.Id, Hammarbysjön.Id, FishSpeciesFrequency.Medium);
                AddRel(lax.Id, Hammarbysjön.Id, FishSpeciesFrequency.Low);
                AddRel(strömming.Id, Hammarbysjön.Id, FishSpeciesFrequency.Low);
                AddRel(öring.Id, Hammarbysjön.Id, FishSpeciesFrequency.Low);
            }

            if (abborre != null && mört != null && gädda != null && Husarviken != null)
            {
                AddRel(abborre.Id, Husarviken.Id, FishSpeciesFrequency.High);
                AddRel(mört.Id, Husarviken.Id, FishSpeciesFrequency.High);
                AddRel(gädda.Id, Husarviken.Id, FishSpeciesFrequency.Low);
            }

            if (mört != null && abborre != null && ruda != null && karp != null && Judarn != null)
            {
                AddRel(mört.Id, Judarn.Id, FishSpeciesFrequency.High);
                AddRel(abborre.Id, Judarn.Id, FishSpeciesFrequency.Medium);
                AddRel(ruda.Id, Judarn.Id, FishSpeciesFrequency.Low);
                AddRel(karp.Id, Judarn.Id, FishSpeciesFrequency.Low);
            }

            if (abborre != null && gädda != null && björkna != null && mört != null && ruda != null && löja != null && sutare != null && Järlasjön != null)
            {
                AddRel(abborre.Id, Järlasjön.Id, FishSpeciesFrequency.High);
                AddRel(gädda.Id, Järlasjön.Id, FishSpeciesFrequency.High);
                AddRel(björkna.Id, Järlasjön.Id, FishSpeciesFrequency.Medium);
                AddRel(mört.Id, Järlasjön.Id, FishSpeciesFrequency.Medium);
                AddRel(ruda.Id, Järlasjön.Id, FishSpeciesFrequency.Low);
                AddRel(löja.Id, Järlasjön.Id, FishSpeciesFrequency.Low);
                AddRel(sutare.Id, Järlasjön.Id, FishSpeciesFrequency.Low);
            }

            if (abborre != null && gädda != null && mört != null && ruda != null && gärs != null && braxen != null && sutare != null && sarv != null && ål != null && björkna != null && Karlbergkanalen != null)
            {
                AddRel(abborre.Id, Karlbergkanalen.Id, FishSpeciesFrequency.High);
                AddRel(gädda.Id, Karlbergkanalen.Id, FishSpeciesFrequency.Medium);
                AddRel(mört.Id, Karlbergkanalen.Id, FishSpeciesFrequency.Medium);
                AddRel(ruda.Id, Karlbergkanalen.Id, FishSpeciesFrequency.Medium);
                AddRel(gärs.Id, Karlbergkanalen.Id, FishSpeciesFrequency.Medium);
                AddRel(braxen.Id, Karlbergkanalen.Id, FishSpeciesFrequency.Low);
                AddRel(sutare.Id, Karlbergkanalen.Id, FishSpeciesFrequency.Low);
                AddRel(sarv.Id, Karlbergkanalen.Id, FishSpeciesFrequency.Low);
                AddRel(ål.Id, Karlbergkanalen.Id, FishSpeciesFrequency.Low);
                AddRel(björkna.Id, Karlbergkanalen.Id, FishSpeciesFrequency.Low);
            }

            if (abborre != null && gädda != null && mört != null && ruda != null && gärs != null && braxen != null && sutare != null && sarv != null && ål != null && björkna != null && Klarasjön != null)
            {
                AddRel(abborre.Id, Klarasjön.Id, FishSpeciesFrequency.High);
                AddRel(gädda.Id, Klarasjön.Id, FishSpeciesFrequency.Medium);
                AddRel(mört.Id, Klarasjön.Id, FishSpeciesFrequency.Medium);
                AddRel(gärs.Id, Klarasjön.Id, FishSpeciesFrequency.Medium);
                AddRel(braxen.Id, Klarasjön.Id, FishSpeciesFrequency.Low);
                AddRel(sutare.Id, Klarasjön.Id, FishSpeciesFrequency.Low);
                AddRel(sarv.Id, Klarasjön.Id, FishSpeciesFrequency.Low);
                AddRel(ål.Id, Klarasjön.Id, FishSpeciesFrequency.Low);
                AddRel(björkna.Id, Klarasjön.Id, FishSpeciesFrequency.Low);
            }

            if (ruda != null && sutare != null && abborre != null && gädda != null && mört != null && Kyrksjön != null)
            {
                AddRel(ruda.Id, Kyrksjön.Id, FishSpeciesFrequency.High);
                AddRel(sutare.Id, Kyrksjön.Id, FishSpeciesFrequency.Medium);
                AddRel(abborre.Id, Kyrksjön.Id, FishSpeciesFrequency.Low);
                AddRel(gädda.Id, Kyrksjön.Id, FishSpeciesFrequency.Low);
                AddRel(mört.Id, Kyrksjön.Id, FishSpeciesFrequency.Low);
            }

            if (abborre != null && gädda != null && regnbåge != null && kräfta != null && Källtorpssjön != null)
            {
                AddRel(abborre.Id, Källtorpssjön.Id, FishSpeciesFrequency.High);
                AddRel(gädda.Id, Källtorpssjön.Id, FishSpeciesFrequency.Medium);
                AddRel(regnbåge.Id, Källtorpssjön.Id, FishSpeciesFrequency.Medium);
                AddRel(kräfta.Id, Källtorpssjön.Id, FishSpeciesFrequency.Low);
            }

            if (gös != null && ruda != null && gädda != null && abborre != null && mört != null && braxen != null && Laduviken != null)
            {
                AddRel(gös.Id, Laduviken.Id, FishSpeciesFrequency.High);
                AddRel(ruda.Id, Laduviken.Id, FishSpeciesFrequency.High);
                AddRel(gädda.Id, Laduviken.Id, FishSpeciesFrequency.Medium);
                AddRel(abborre.Id, Laduviken.Id, FishSpeciesFrequency.Medium);
                AddRel(mört.Id, Laduviken.Id, FishSpeciesFrequency.Low);
                AddRel(braxen.Id, Laduviken.Id, FishSpeciesFrequency.Low);
            }

            if (braxen != null && sarv != null && mört != null && abborre != null && gädda != null && gös != null && Lappkärret != null)
            {
                AddRel(braxen.Id, Lappkärret.Id, FishSpeciesFrequency.High);
                AddRel(sarv.Id, Lappkärret.Id, FishSpeciesFrequency.High);
                AddRel(mört.Id, Lappkärret.Id, FishSpeciesFrequency.High);
                AddRel(abborre.Id, Lappkärret.Id, FishSpeciesFrequency.Medium);
                AddRel(gädda.Id, Lappkärret.Id, FishSpeciesFrequency.Low);
                AddRel(gös.Id, Lappkärret.Id, FishSpeciesFrequency.Low);
            }

            if (gädda != null && abborre != null && gös != null && havsöring != null && mört != null && braxen != null && LillaVärtan != null)
            {
                AddRel(gädda.Id, LillaVärtan.Id, FishSpeciesFrequency.High);
                AddRel(abborre.Id, LillaVärtan.Id, FishSpeciesFrequency.High);
                AddRel(gös.Id, LillaVärtan.Id, FishSpeciesFrequency.Medium);
                AddRel(havsöring.Id, LillaVärtan.Id, FishSpeciesFrequency.Low);
                AddRel(mört.Id, LillaVärtan.Id, FishSpeciesFrequency.Low);
                AddRel(braxen.Id, LillaVärtan.Id, FishSpeciesFrequency.Low);
            }

            if (gös != null && gädda != null && abborre != null && mört != null && Lillsjön != null)
            {
                AddRel(gös.Id, Lillsjön.Id, FishSpeciesFrequency.High);
                AddRel(gädda.Id, Lillsjön.Id, FishSpeciesFrequency.Medium);
                AddRel(abborre.Id, Lillsjön.Id, FishSpeciesFrequency.Medium);
                AddRel(mört.Id, Lillsjön.Id, FishSpeciesFrequency.Low);
            }

            if (ruda != null && sutare != null && karp != null && abborre != null && gädda != null && mört != null && Långsjön_Hanveden != null)
            {
                AddRel(ruda.Id, Långsjön_Hanveden.Id, FishSpeciesFrequency.High);
                AddRel(sutare.Id, Långsjön_Hanveden.Id, FishSpeciesFrequency.High);
                AddRel(karp.Id, Långsjön_Hanveden.Id, FishSpeciesFrequency.High);
                AddRel(abborre.Id, Långsjön_Hanveden.Id, FishSpeciesFrequency.Medium);
                AddRel(gädda.Id, Långsjön_Hanveden.Id, FishSpeciesFrequency.Low);
                AddRel(mört.Id, Långsjön_Hanveden.Id, FishSpeciesFrequency.Low);
            }

            if (abborre != null && gädda != null && gös != null && mört != null && karp != null && Långsjön_Älvsjö != null)
            {
                AddRel(abborre.Id, Långsjön_Älvsjö.Id, FishSpeciesFrequency.High);
                AddRel(gädda.Id, Långsjön_Älvsjö.Id, FishSpeciesFrequency.Medium);
                AddRel(gös.Id, Långsjön_Älvsjö.Id, FishSpeciesFrequency.Medium);
                AddRel(mört.Id, Långsjön_Älvsjö.Id, FishSpeciesFrequency.Medium);
                AddRel(karp.Id, Långsjön_Älvsjö.Id, FishSpeciesFrequency.Low);
            }

            if (gös != null && gädda != null && Magelungen != null)
            {
                AddRel(gös.Id, Magelungen.Id, FishSpeciesFrequency.High);
                AddRel(gädda.Id, Magelungen.Id, FishSpeciesFrequency.High);
            }

            if (karp != null && abborre != null && gädda != null && mört != null && sutare != null && Mörtsjön != null)
            {
                AddRel(karp.Id, Mörtsjön.Id, FishSpeciesFrequency.High);
                AddRel(abborre.Id, Mörtsjön.Id, FishSpeciesFrequency.Medium);
                AddRel(gädda.Id, Mörtsjön.Id, FishSpeciesFrequency.Medium);
                AddRel(mört.Id, Mörtsjön.Id, FishSpeciesFrequency.Medium);
                AddRel(sutare.Id, Mörtsjön.Id, FishSpeciesFrequency.Low);
            }

            if (gädda != null && abborre != null && regnbåge != null && öring != null && mört != null && braxen != null && sutare != null && Nacka_Ström != null)
            {
                AddRel(gädda.Id, Nacka_Ström.Id, FishSpeciesFrequency.High);
                AddRel(abborre.Id, Nacka_Ström.Id, FishSpeciesFrequency.High);
                AddRel(regnbåge.Id, Nacka_Ström.Id, FishSpeciesFrequency.Medium);
                AddRel(öring.Id, Nacka_Ström.Id, FishSpeciesFrequency.Medium);
                AddRel(mört.Id, Nacka_Ström.Id, FishSpeciesFrequency.Low);
                AddRel(braxen.Id, Nacka_Ström.Id, FishSpeciesFrequency.Low);
                AddRel(sutare.Id, Nacka_Ström.Id, FishSpeciesFrequency.Low);
            }

            if (gädda != null && abborre != null && gös != null && mört != null && braxen != null && sarv != null && nors != null && gers != null && ruda != null && lake != null && Orlången != null)
            {
                AddRel(gädda.Id, Orlången.Id, FishSpeciesFrequency.High);
                AddRel(abborre.Id, Orlången.Id, FishSpeciesFrequency.High);
                AddRel(gös.Id, Orlången.Id, FishSpeciesFrequency.High);
                AddRel(mört.Id, Orlången.Id, FishSpeciesFrequency.Medium);
                AddRel(braxen.Id, Orlången.Id, FishSpeciesFrequency.Medium);
                AddRel(sarv.Id, Orlången.Id, FishSpeciesFrequency.Low);
                AddRel(nors.Id, Orlången.Id, FishSpeciesFrequency.Low);
                AddRel(gers.Id, Orlången.Id, FishSpeciesFrequency.Low);
                AddRel(ruda.Id, Orlången.Id, FishSpeciesFrequency.Low);
                AddRel(lake.Id, Orlången.Id, FishSpeciesFrequency.Low);
            }

            if (abborre != null && gös != null && havsöring != null && gädda != null && asp != null && Riddarfjärden != null)
            {
                AddRel(abborre.Id, Riddarfjärden.Id, FishSpeciesFrequency.High);
                AddRel(gös.Id, Riddarfjärden.Id, FishSpeciesFrequency.High);
                AddRel(havsöring.Id, Riddarfjärden.Id, FishSpeciesFrequency.Medium);
                AddRel(gädda.Id, Riddarfjärden.Id, FishSpeciesFrequency.Medium);
                AddRel(asp.Id, Riddarfjärden.Id, FishSpeciesFrequency.Low);
            }

            if (sutare != null && abborre != null && mört != null && ruda != null && karp != null && Råcksta_Träsk != null)
            {
                AddRel(sutare.Id, Råcksta_Träsk.Id, FishSpeciesFrequency.High);
                AddRel(abborre.Id, Råcksta_Träsk.Id, FishSpeciesFrequency.High);
                AddRel(mört.Id, Råcksta_Träsk.Id, FishSpeciesFrequency.Medium);
                AddRel(ruda.Id, Råcksta_Träsk.Id, FishSpeciesFrequency.Medium);
                AddRel(karp.Id, Råcksta_Träsk.Id, FishSpeciesFrequency.Low);
            }

            if (abborre != null && gädda != null && mört != null && löja != null && Rönningesjön != null)
            {
                AddRel(abborre.Id, Rönningesjön.Id, FishSpeciesFrequency.High);
                AddRel(gädda.Id, Rönningesjön.Id, FishSpeciesFrequency.High);
                AddRel(mört.Id, Rönningesjön.Id, FishSpeciesFrequency.Medium);
                AddRel(löja.Id, Rönningesjön.Id, FishSpeciesFrequency.Low);
            }

            if (abborre != null && gädda != null && mört != null && braxen != null && sarv != null && sutare != null && lake != null && nissöga != null && björkna != null && löja != null && Rösjön != null)
            {
                AddRel(abborre.Id, Rösjön.Id, FishSpeciesFrequency.High);
                AddRel(gädda.Id, Rösjön.Id, FishSpeciesFrequency.Medium);
                AddRel(mört.Id, Rösjön.Id, FishSpeciesFrequency.Low);
                AddRel(braxen.Id, Rösjön.Id, FishSpeciesFrequency.Low);
                AddRel(sarv.Id, Rösjön.Id, FishSpeciesFrequency.Low);
                AddRel(sutare.Id, Rösjön.Id, FishSpeciesFrequency.Low);
                AddRel(lake.Id, Rösjön.Id, FishSpeciesFrequency.Low);
                AddRel(nissöga.Id, Rösjön.Id, FishSpeciesFrequency.Low);
                AddRel(björkna.Id, Rösjön.Id, FishSpeciesFrequency.Low);
                AddRel(löja.Id, Rösjön.Id, FishSpeciesFrequency.Low);
            }

            if (gädda != null && abborre != null && gös != null && strömming != null && havsöring != null && sik != null && nors != null && braxen != null && Saltsjön != null)
            {
                AddRel(gädda.Id, Saltsjön.Id, FishSpeciesFrequency.High);
                AddRel(abborre.Id, Saltsjön.Id, FishSpeciesFrequency.High);
                AddRel(gös.Id, Saltsjön.Id, FishSpeciesFrequency.Medium);
                AddRel(strömming.Id, Saltsjön.Id, FishSpeciesFrequency.Medium);
                AddRel(havsöring.Id, Saltsjön.Id, FishSpeciesFrequency.Medium);
                AddRel(sik.Id, Saltsjön.Id, FishSpeciesFrequency.Low);
                AddRel(nors.Id, Saltsjön.Id, FishSpeciesFrequency.Low);
                AddRel(braxen.Id, Saltsjön.Id, FishSpeciesFrequency.Low);
            }

            if (gädda != null && abborre != null && mört != null && braxen != null && björkna != null && sutare != null && Sicklasjön != null)
            {
                AddRel(gädda.Id, Sicklasjön.Id, FishSpeciesFrequency.High);
                AddRel(abborre.Id, Sicklasjön.Id, FishSpeciesFrequency.Medium);
                AddRel(mört.Id, Sicklasjön.Id, FishSpeciesFrequency.Low);
                AddRel(braxen.Id, Sicklasjön.Id, FishSpeciesFrequency.Low);
                AddRel(björkna.Id, Sicklasjön.Id, FishSpeciesFrequency.Low);
                AddRel(sutare.Id, Sicklasjön.Id, FishSpeciesFrequency.Low);
            }

            if (regnbåge != null && Spegeldammen != null)
                AddRel(regnbåge.Id, Spegeldammen.Id, FishSpeciesFrequency.High);

            if (gädda != null && abborre != null && havsöring != null && sik != null && strömming != null && gös != null && Svindersviken != null)
            {
                AddRel(gädda.Id, Svindersviken.Id, FishSpeciesFrequency.High);
                AddRel(abborre.Id, Svindersviken.Id, FishSpeciesFrequency.High);
                AddRel(havsöring.Id, Svindersviken.Id, FishSpeciesFrequency.Medium);
                AddRel(sik.Id, Svindersviken.Id, FishSpeciesFrequency.Medium);
                AddRel(strömming.Id, Svindersviken.Id, FishSpeciesFrequency.Medium);
                AddRel(gös.Id, Svindersviken.Id, FishSpeciesFrequency.Low);
            }

            if (abborre != null && gädda != null && mört != null && braxen != null && sarv != null && björkna != null && Säbysjön != null)
            {
                AddRel(abborre.Id, Säbysjön.Id, FishSpeciesFrequency.High);
                AddRel(gädda.Id, Säbysjön.Id, FishSpeciesFrequency.Medium);
                AddRel(mört.Id, Säbysjön.Id, FishSpeciesFrequency.Low);
                AddRel(braxen.Id, Säbysjön.Id, FishSpeciesFrequency.Low);
                AddRel(sarv.Id, Säbysjön.Id, FishSpeciesFrequency.Low);
                AddRel(björkna.Id, Säbysjön.Id, FishSpeciesFrequency.Low);
            }

            if (gädda != null && gös != null && abborre != null && mört != null && braxen != null && havsöring != null && Sätraån != null)
            {
                AddRel(gädda.Id, Sätraån.Id, FishSpeciesFrequency.High);
                AddRel(gös.Id, Sätraån.Id, FishSpeciesFrequency.High);
                AddRel(abborre.Id, Sätraån.Id, FishSpeciesFrequency.High);
                AddRel(mört.Id, Sätraån.Id, FishSpeciesFrequency.Medium);
                AddRel(braxen.Id, Sätraån.Id, FishSpeciesFrequency.Medium);
                AddRel(havsöring.Id, Sätraån.Id, FishSpeciesFrequency.Low);
            }

            if (ruda != null && abborre != null && gädda != null && karp != null && Söderbysjön != null)
            {
                AddRel(ruda.Id, Söderbysjön.Id, FishSpeciesFrequency.High);
                AddRel(abborre.Id, Söderbysjön.Id, FishSpeciesFrequency.Medium);
                AddRel(gädda.Id, Söderbysjön.Id, FishSpeciesFrequency.Medium);
                AddRel(karp.Id, Söderbysjön.Id, FishSpeciesFrequency.Low);
            }

            if (karp != null && abborre != null && gädda != null && ruda != null && mört != null && regnbåge != null && Trekanten != null)
            {
                AddRel(karp.Id, Trekanten.Id, FishSpeciesFrequency.High);
                AddRel(abborre.Id, Trekanten.Id, FishSpeciesFrequency.Medium);
                AddRel(gädda.Id, Trekanten.Id, FishSpeciesFrequency.Medium);
                AddRel(ruda.Id, Trekanten.Id, FishSpeciesFrequency.Medium);
                AddRel(mört.Id, Trekanten.Id, FishSpeciesFrequency.Medium);
                AddRel(regnbåge.Id, Trekanten.Id, FishSpeciesFrequency.Low);
            }

            if (gädda != null && abborre != null && regnbåge != null && sutare != null && ruda != null && Trehörningen != null)
            {
                AddRel(gädda.Id, Trehörningen.Id, FishSpeciesFrequency.High);
                AddRel(abborre.Id, Trehörningen.Id, FishSpeciesFrequency.High);
                AddRel(regnbåge.Id, Trehörningen.Id, FishSpeciesFrequency.Low);
                AddRel(sutare.Id, Trehörningen.Id, FishSpeciesFrequency.Low);
                AddRel(ruda.Id, Trehörningen.Id, FishSpeciesFrequency.Low);
            }

            if (abborre != null && braxen != null && mört != null && gädda != null && löja != null && sarv != null && ål != null && gös != null && lake != null && Tullingesjön != null)
            {
                AddRel(abborre.Id, Tullingesjön.Id, FishSpeciesFrequency.High);
                AddRel(braxen.Id, Tullingesjön.Id, FishSpeciesFrequency.High);
                AddRel(mört.Id, Tullingesjön.Id, FishSpeciesFrequency.High);
                AddRel(gädda.Id, Tullingesjön.Id, FishSpeciesFrequency.Medium);
                AddRel(löja.Id, Tullingesjön.Id, FishSpeciesFrequency.Low);
                AddRel(sarv.Id, Tullingesjön.Id, FishSpeciesFrequency.Low);
                AddRel(ål.Id, Tullingesjön.Id, FishSpeciesFrequency.Low);
                AddRel(gös.Id, Tullingesjön.Id, FishSpeciesFrequency.Low);
                AddRel(lake.Id, Tullingesjön.Id, FishSpeciesFrequency.Low);
            }

            if (gädda != null && abborre != null && gös != null && braxen != null && mört != null && Ullnassjön != null)
            {
                AddRel(gädda.Id, Ullnassjön.Id, FishSpeciesFrequency.High);
                AddRel(abborre.Id, Ullnassjön.Id, FishSpeciesFrequency.High);
                AddRel(gös.Id, Ullnassjön.Id, FishSpeciesFrequency.Medium);
                AddRel(braxen.Id, Ullnassjön.Id, FishSpeciesFrequency.Low);
                AddRel(mört.Id, Ullnassjön.Id, FishSpeciesFrequency.Low);
            }

            if (abborre != null && gädda != null && Ulvsjön != null)
            {
                AddRel(abborre.Id, Ulvsjön.Id, FishSpeciesFrequency.High);
                AddRel(gädda.Id, Ulvsjön.Id, FishSpeciesFrequency.Medium);
            }

            if (gös != null && gädda != null && abborre != null && Ulvsundasjön != null)
            {
                AddRel(gös.Id, Ulvsundasjön.Id, FishSpeciesFrequency.High);
                AddRel(gädda.Id, Ulvsundasjön.Id, FishSpeciesFrequency.Medium);
                AddRel(abborre.Id, Ulvsundasjön.Id, FishSpeciesFrequency.Medium);
            }

            if (abborre != null && gädda != null && gös != null && björkna != null && braxen != null && gärs != null && Uttran != null)
            {
                AddRel(abborre.Id, Uttran.Id, FishSpeciesFrequency.High);
                AddRel(gädda.Id, Uttran.Id, FishSpeciesFrequency.Low);
                AddRel(gös.Id, Uttran.Id, FishSpeciesFrequency.Low);
                AddRel(björkna.Id, Uttran.Id, FishSpeciesFrequency.Low);
                AddRel(braxen.Id, Uttran.Id, FishSpeciesFrequency.Low);
                AddRel(gärs.Id, Uttran.Id, FishSpeciesFrequency.Low);
            }

            if (gös != null && abborre != null && gädda != null && asp != null && ål != null && Vallentunasjön != null)
            {
                AddRel(gös.Id, Vallentunasjön.Id, FishSpeciesFrequency.High);
                AddRel(abborre.Id, Vallentunasjön.Id, FishSpeciesFrequency.High);
                AddRel(gädda.Id, Vallentunasjön.Id, FishSpeciesFrequency.Medium);
                AddRel(asp.Id, Vallentunasjön.Id, FishSpeciesFrequency.Low);
                AddRel(ål.Id, Vallentunasjön.Id, FishSpeciesFrequency.Low);
            }

            if (abborre != null && gös != null && gädda != null && mört != null && braxen != null && sik != null && siklöja != null && Vinterviken != null)
            {
                AddRel(abborre.Id, Vinterviken.Id, FishSpeciesFrequency.High);
                AddRel(gös.Id, Vinterviken.Id, FishSpeciesFrequency.Medium);
                AddRel(gädda.Id, Vinterviken.Id, FishSpeciesFrequency.Medium);
                AddRel(mört.Id, Vinterviken.Id, FishSpeciesFrequency.Medium);
                AddRel(braxen.Id, Vinterviken.Id, FishSpeciesFrequency.Medium);
                AddRel(sik.Id, Vinterviken.Id, FishSpeciesFrequency.Low);
                AddRel(siklöja.Id, Vinterviken.Id, FishSpeciesFrequency.Low);
            }

            if (mört != null && braxen != null && gädda != null && abborre != null && Ådran != null)
            {
                AddRel(mört.Id, Ådran.Id, FishSpeciesFrequency.High);
                AddRel(braxen.Id, Ådran.Id, FishSpeciesFrequency.High);
                AddRel(gädda.Id, Ådran.Id, FishSpeciesFrequency.Medium);
                AddRel(abborre.Id, Ådran.Id, FishSpeciesFrequency.Medium);
            }

            if (abborre != null && mört != null && gädda != null && ruda != null && gärs != null && braxen != null && sarv != null && björkna != null && Årstaviken != null)
            {
                AddRel(abborre.Id, Årstaviken.Id, FishSpeciesFrequency.High);
                AddRel(mört.Id, Årstaviken.Id, FishSpeciesFrequency.High);
                AddRel(gädda.Id, Årstaviken.Id, FishSpeciesFrequency.Medium);
                AddRel(ruda.Id, Årstaviken.Id, FishSpeciesFrequency.Low);
                AddRel(gärs.Id, Årstaviken.Id, FishSpeciesFrequency.Low);
                AddRel(braxen.Id, Årstaviken.Id, FishSpeciesFrequency.Low);
                AddRel(sarv.Id, Årstaviken.Id, FishSpeciesFrequency.Low);
                AddRel(björkna.Id, Årstaviken.Id, FishSpeciesFrequency.Low);
            }

            if (abborre != null && gädda != null && gös != null && Ältasjön != null)
            {
                AddRel(abborre.Id, Ältasjön.Id, FishSpeciesFrequency.High);
                AddRel(gädda.Id, Ältasjön.Id, FishSpeciesFrequency.Low);
                AddRel(gös.Id, Ältasjön.Id, FishSpeciesFrequency.Low);
            }

            if (gös != null && abborre != null && gädda != null && braxen != null && mört != null && björkna != null && sutare != null && öring != null && lax != null && Östra_Mälaren != null)
            {
                AddRel(gös.Id, Östra_Mälaren.Id, FishSpeciesFrequency.High);
                AddRel(abborre.Id, Östra_Mälaren.Id, FishSpeciesFrequency.High);
                AddRel(gädda.Id, Östra_Mälaren.Id, FishSpeciesFrequency.High);
                AddRel(braxen.Id, Östra_Mälaren.Id, FishSpeciesFrequency.Medium);
                AddRel(mört.Id, Östra_Mälaren.Id, FishSpeciesFrequency.Medium);
                AddRel(björkna.Id, Östra_Mälaren.Id, FishSpeciesFrequency.Medium);
                AddRel(sutare.Id, Östra_Mälaren.Id, FishSpeciesFrequency.Medium);
                AddRel(öring.Id, Östra_Mälaren.Id, FishSpeciesFrequency.Low);
                AddRel(lax.Id, Östra_Mälaren.Id, FishSpeciesFrequency.Low);
            }

            if (abborre != null && gädda != null && mört != null && braxen != null && sarv != null && björkna != null && Översjön != null)
            {
                AddRel(abborre.Id, Översjön.Id, FishSpeciesFrequency.High);
                AddRel(gädda.Id, Översjön.Id, FishSpeciesFrequency.High);
                AddRel(mört.Id, Översjön.Id, FishSpeciesFrequency.High);
                AddRel(braxen.Id, Översjön.Id, FishSpeciesFrequency.Medium);
                AddRel(sarv.Id, Översjön.Id, FishSpeciesFrequency.Low);
                AddRel(björkna.Id, Översjön.Id, FishSpeciesFrequency.Low);
            }

            foreach (var rel in relationships)
            {
                db.FishingSpeciesFishingSpots.Add(new FishingSpeciesFishingSpot
                {
                    FishSpeciesId = rel.speciesId,
                    FishingSpotId = rel.spotId,
                    FishSpeciesFrequencyId = (int)rel.freq,
                    Frequency = rel.freq
                });
            }
            db.SaveChanges();
        }
    }
}
