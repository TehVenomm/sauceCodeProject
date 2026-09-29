.class public Lnet/gogame/gowrap/integrations/core/ServerStatus$LocalizedStatus;
.super Ljava/lang/Object;
.source "ServerStatus.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/gowrap/integrations/core/ServerStatus;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x9
    name = "LocalizedStatus"
.end annotation


# instance fields
.field private faq:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Lnet/gogame/gowrap/integrations/core/ServerStatus$StatusFaqEntry;",
            ">;"
        }
    .end annotation
.end field

.field private message:Ljava/lang/String;

.field private title:Ljava/lang/String;

.field private url:Ljava/lang/String;


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 31
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public getFaq()Ljava/util/List;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Lnet/gogame/gowrap/integrations/core/ServerStatus$StatusFaqEntry;",
            ">;"
        }
    .end annotation

    .line 63
    iget-object v0, p0, Lnet/gogame/gowrap/integrations/core/ServerStatus$LocalizedStatus;->faq:Ljava/util/List;

    return-object v0
.end method

.method public getMessage()Ljava/lang/String;
    .locals 1

    .line 47
    iget-object v0, p0, Lnet/gogame/gowrap/integrations/core/ServerStatus$LocalizedStatus;->message:Ljava/lang/String;

    return-object v0
.end method

.method public getTitle()Ljava/lang/String;
    .locals 1

    .line 39
    iget-object v0, p0, Lnet/gogame/gowrap/integrations/core/ServerStatus$LocalizedStatus;->title:Ljava/lang/String;

    return-object v0
.end method

.method public getUrl()Ljava/lang/String;
    .locals 1

    .line 55
    iget-object v0, p0, Lnet/gogame/gowrap/integrations/core/ServerStatus$LocalizedStatus;->url:Ljava/lang/String;

    return-object v0
.end method

.method public setFaq(Ljava/util/List;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lnet/gogame/gowrap/integrations/core/ServerStatus$StatusFaqEntry;",
            ">;)V"
        }
    .end annotation

    .line 67
    iput-object p1, p0, Lnet/gogame/gowrap/integrations/core/ServerStatus$LocalizedStatus;->faq:Ljava/util/List;

    return-void
.end method

.method public setMessage(Ljava/lang/String;)V
    .locals 0

    .line 51
    iput-object p1, p0, Lnet/gogame/gowrap/integrations/core/ServerStatus$LocalizedStatus;->message:Ljava/lang/String;

    return-void
.end method

.method public setTitle(Ljava/lang/String;)V
    .locals 0

    .line 43
    iput-object p1, p0, Lnet/gogame/gowrap/integrations/core/ServerStatus$LocalizedStatus;->title:Ljava/lang/String;

    return-void
.end method

.method public setUrl(Ljava/lang/String;)V
    .locals 0

    .line 59
    iput-object p1, p0, Lnet/gogame/gowrap/integrations/core/ServerStatus$LocalizedStatus;->url:Ljava/lang/String;

    return-void
.end method
