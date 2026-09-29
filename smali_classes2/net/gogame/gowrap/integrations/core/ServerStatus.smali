.class public Lnet/gogame/gowrap/integrations/core/ServerStatus;
.super Ljava/lang/Object;
.source "ServerStatus.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lnet/gogame/gowrap/integrations/core/ServerStatus$StatusFaqEntry;,
        Lnet/gogame/gowrap/integrations/core/ServerStatus$LocalizedStatus;,
        Lnet/gogame/gowrap/integrations/core/ServerStatus$Status;
    }
.end annotation


# instance fields
.field private locales:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Lnet/gogame/gowrap/integrations/core/ServerStatus$LocalizedStatus;",
            ">;"
        }
    .end annotation
.end field

.field private status:Lnet/gogame/gowrap/integrations/core/ServerStatus$Status;


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 6
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public getLocales()Ljava/util/Map;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Lnet/gogame/gowrap/integrations/core/ServerStatus$LocalizedStatus;",
            ">;"
        }
    .end annotation

    .line 20
    iget-object v0, p0, Lnet/gogame/gowrap/integrations/core/ServerStatus;->locales:Ljava/util/Map;

    return-object v0
.end method

.method public getStatus()Lnet/gogame/gowrap/integrations/core/ServerStatus$Status;
    .locals 1

    .line 12
    iget-object v0, p0, Lnet/gogame/gowrap/integrations/core/ServerStatus;->status:Lnet/gogame/gowrap/integrations/core/ServerStatus$Status;

    return-object v0
.end method

.method public setLocales(Ljava/util/Map;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Lnet/gogame/gowrap/integrations/core/ServerStatus$LocalizedStatus;",
            ">;)V"
        }
    .end annotation

    .line 24
    iput-object p1, p0, Lnet/gogame/gowrap/integrations/core/ServerStatus;->locales:Ljava/util/Map;

    return-void
.end method

.method public setStatus(Lnet/gogame/gowrap/integrations/core/ServerStatus$Status;)V
    .locals 0

    .line 16
    iput-object p1, p0, Lnet/gogame/gowrap/integrations/core/ServerStatus;->status:Lnet/gogame/gowrap/integrations/core/ServerStatus$Status;

    return-void
.end method
