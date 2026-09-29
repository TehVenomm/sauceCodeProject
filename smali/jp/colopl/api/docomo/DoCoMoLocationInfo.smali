.class public Ljp/colopl/api/docomo/DoCoMoLocationInfo;
.super Ljava/lang/Object;
.source "DoCoMoLocationInfo.java"


# instance fields
.field private featureList:Ljava/util/ArrayList;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/ArrayList<",
            "Ljp/colopl/api/docomo/Feature;",
            ">;"
        }
    .end annotation
.end field

.field private resultInfo:Ljp/colopl/api/docomo/ResultInfo;


# direct methods
.method public constructor <init>()V
    .locals 2

    .line 20
    new-instance v0, Ljp/colopl/api/docomo/ResultInfo;

    invoke-direct {v0}, Ljp/colopl/api/docomo/ResultInfo;-><init>()V

    new-instance v1, Ljava/util/ArrayList;

    invoke-direct {v1}, Ljava/util/ArrayList;-><init>()V

    invoke-direct {p0, v0, v1}, Ljp/colopl/api/docomo/DoCoMoLocationInfo;-><init>(Ljp/colopl/api/docomo/ResultInfo;Ljava/util/ArrayList;)V

    return-void
.end method

.method public constructor <init>(Ljp/colopl/api/docomo/ResultInfo;)V
    .locals 1

    .line 11
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    invoke-direct {p0, p1, v0}, Ljp/colopl/api/docomo/DoCoMoLocationInfo;-><init>(Ljp/colopl/api/docomo/ResultInfo;Ljava/util/ArrayList;)V

    return-void
.end method

.method public constructor <init>(Ljp/colopl/api/docomo/ResultInfo;Ljava/util/ArrayList;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljp/colopl/api/docomo/ResultInfo;",
            "Ljava/util/ArrayList<",
            "Ljp/colopl/api/docomo/Feature;",
            ">;)V"
        }
    .end annotation

    .line 14
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 15
    iput-object p1, p0, Ljp/colopl/api/docomo/DoCoMoLocationInfo;->resultInfo:Ljp/colopl/api/docomo/ResultInfo;

    .line 16
    iput-object p2, p0, Ljp/colopl/api/docomo/DoCoMoLocationInfo;->featureList:Ljava/util/ArrayList;

    return-void
.end method


# virtual methods
.method public getFeature()Ljp/colopl/api/docomo/Feature;
    .locals 1

    const/4 v0, 0x0

    .line 36
    invoke-virtual {p0, v0}, Ljp/colopl/api/docomo/DoCoMoLocationInfo;->getFeature(I)Ljp/colopl/api/docomo/Feature;

    move-result-object v0

    return-object v0
.end method

.method public getFeature(I)Ljp/colopl/api/docomo/Feature;
    .locals 1

    .line 40
    iget-object v0, p0, Ljp/colopl/api/docomo/DoCoMoLocationInfo;->featureList:Ljava/util/ArrayList;

    if-eqz v0, :cond_0

    iget-object v0, p0, Ljp/colopl/api/docomo/DoCoMoLocationInfo;->featureList:Ljava/util/ArrayList;

    invoke-virtual {v0}, Ljava/util/ArrayList;->size()I

    move-result v0

    add-int/lit8 v0, v0, -0x1

    if-gt p1, v0, :cond_0

    .line 41
    iget-object v0, p0, Ljp/colopl/api/docomo/DoCoMoLocationInfo;->featureList:Ljava/util/ArrayList;

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->get(I)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Ljp/colopl/api/docomo/Feature;

    return-object p1

    :cond_0
    const/4 p1, 0x0

    return-object p1
.end method

.method public getFeatureList()Ljava/util/ArrayList;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/ArrayList<",
            "Ljp/colopl/api/docomo/Feature;",
            ">;"
        }
    .end annotation

    .line 32
    iget-object v0, p0, Ljp/colopl/api/docomo/DoCoMoLocationInfo;->featureList:Ljava/util/ArrayList;

    return-object v0
.end method

.method public getResultInfo()Ljp/colopl/api/docomo/ResultInfo;
    .locals 1

    .line 28
    iget-object v0, p0, Ljp/colopl/api/docomo/DoCoMoLocationInfo;->resultInfo:Ljp/colopl/api/docomo/ResultInfo;

    return-object v0
.end method

.method public setResultInfo(Ljp/colopl/api/docomo/ResultInfo;)V
    .locals 0

    .line 24
    iput-object p1, p0, Ljp/colopl/api/docomo/DoCoMoLocationInfo;->resultInfo:Ljp/colopl/api/docomo/ResultInfo;

    return-void
.end method

.method public toString()Ljava/lang/String;
    .locals 3

    .line 47
    new-instance v0, Ljava/lang/StringBuffer;

    invoke-direct {v0}, Ljava/lang/StringBuffer;-><init>()V

    const-string v1, "["

    .line 48
    invoke-virtual {v0, v1}, Ljava/lang/StringBuffer;->append(Ljava/lang/String;)Ljava/lang/StringBuffer;

    .line 49
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "ResultInfo: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v2, p0, Ljp/colopl/api/docomo/DoCoMoLocationInfo;->resultInfo:Ljp/colopl/api/docomo/ResultInfo;

    if-nez v2, :cond_0

    const-string v2, "null"

    goto :goto_0

    :cond_0
    iget-object v2, p0, Ljp/colopl/api/docomo/DoCoMoLocationInfo;->resultInfo:Ljp/colopl/api/docomo/ResultInfo;

    invoke-virtual {v2}, Ljp/colopl/api/docomo/ResultInfo;->toString()Ljava/lang/String;

    move-result-object v2

    :goto_0
    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, ", "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuffer;->append(Ljava/lang/String;)Ljava/lang/StringBuffer;

    .line 50
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "FeatureList: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v2, p0, Ljp/colopl/api/docomo/DoCoMoLocationInfo;->featureList:Ljava/util/ArrayList;

    if-nez v2, :cond_1

    const-string v2, "null"

    goto :goto_1

    :cond_1
    iget-object v2, p0, Ljp/colopl/api/docomo/DoCoMoLocationInfo;->featureList:Ljava/util/ArrayList;

    invoke-virtual {v2}, Ljava/util/ArrayList;->toString()Ljava/lang/String;

    move-result-object v2

    :goto_1
    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuffer;->append(Ljava/lang/String;)Ljava/lang/StringBuffer;

    const-string v1, "]"

    .line 51
    invoke-virtual {v0, v1}, Ljava/lang/StringBuffer;->append(Ljava/lang/String;)Ljava/lang/StringBuffer;

    .line 52
    invoke-virtual {v0}, Ljava/lang/StringBuffer;->toString()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method
