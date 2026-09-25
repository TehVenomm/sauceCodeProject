.class public Lnet/gogame/gowrap/integrations/core/ServerStatus$StatusFaqEntry;
.super Ljava/lang/Object;
.source "ServerStatus.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/gowrap/integrations/core/ServerStatus;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x9
    name = "StatusFaqEntry"
.end annotation


# instance fields
.field private answer:Ljava/lang/String;

.field private question:Ljava/lang/String;


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 71
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public getAnswer()Ljava/lang/String;
    .locals 1

    .line 85
    iget-object v0, p0, Lnet/gogame/gowrap/integrations/core/ServerStatus$StatusFaqEntry;->answer:Ljava/lang/String;

    return-object v0
.end method

.method public getQuestion()Ljava/lang/String;
    .locals 1

    .line 77
    iget-object v0, p0, Lnet/gogame/gowrap/integrations/core/ServerStatus$StatusFaqEntry;->question:Ljava/lang/String;

    return-object v0
.end method

.method public setAnswer(Ljava/lang/String;)V
    .locals 0

    .line 89
    iput-object p1, p0, Lnet/gogame/gowrap/integrations/core/ServerStatus$StatusFaqEntry;->answer:Ljava/lang/String;

    return-void
.end method

.method public setQuestion(Ljava/lang/String;)V
    .locals 0

    .line 81
    iput-object p1, p0, Lnet/gogame/gowrap/integrations/core/ServerStatus$StatusFaqEntry;->question:Ljava/lang/String;

    return-void
.end method
