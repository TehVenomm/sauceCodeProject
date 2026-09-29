.class public final Lcom/helpshift/HelpshiftUser$Builder;
.super Ljava/lang/Object;
.source "HelpshiftUser.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/HelpshiftUser;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x19
    name = "Builder"
.end annotation


# instance fields
.field private authToken:Ljava/lang/String;

.field private email:Ljava/lang/String;

.field private identifier:Ljava/lang/String;

.field private name:Ljava/lang/String;


# direct methods
.method public constructor <init>(Ljava/lang/String;Ljava/lang/String;)V
    .locals 1

    .line 44
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const/4 v0, 0x0

    .line 39
    iput-object v0, p0, Lcom/helpshift/HelpshiftUser$Builder;->identifier:Ljava/lang/String;

    .line 40
    iput-object v0, p0, Lcom/helpshift/HelpshiftUser$Builder;->email:Ljava/lang/String;

    .line 46
    invoke-static {p1}, Lcom/helpshift/util/HSPattern;->isValidLoginIdentifier(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 47
    invoke-static {p2}, Lcom/helpshift/util/HSPattern;->isValidLoginEmail(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 48
    iput-object p1, p0, Lcom/helpshift/HelpshiftUser$Builder;->identifier:Ljava/lang/String;

    .line 49
    iput-object p2, p0, Lcom/helpshift/HelpshiftUser$Builder;->email:Ljava/lang/String;

    :cond_0
    return-void
.end method

.method static synthetic access$000(Lcom/helpshift/HelpshiftUser$Builder;)Ljava/lang/String;
    .locals 0

    .line 38
    iget-object p0, p0, Lcom/helpshift/HelpshiftUser$Builder;->identifier:Ljava/lang/String;

    return-object p0
.end method

.method static synthetic access$100(Lcom/helpshift/HelpshiftUser$Builder;)Ljava/lang/String;
    .locals 0

    .line 38
    iget-object p0, p0, Lcom/helpshift/HelpshiftUser$Builder;->email:Ljava/lang/String;

    return-object p0
.end method

.method static synthetic access$200(Lcom/helpshift/HelpshiftUser$Builder;)Ljava/lang/String;
    .locals 0

    .line 38
    iget-object p0, p0, Lcom/helpshift/HelpshiftUser$Builder;->name:Ljava/lang/String;

    return-object p0
.end method

.method static synthetic access$300(Lcom/helpshift/HelpshiftUser$Builder;)Ljava/lang/String;
    .locals 0

    .line 38
    iget-object p0, p0, Lcom/helpshift/HelpshiftUser$Builder;->authToken:Ljava/lang/String;

    return-object p0
.end method


# virtual methods
.method public build()Lcom/helpshift/HelpshiftUser;
    .locals 2

    .line 64
    new-instance v0, Lcom/helpshift/HelpshiftUser;

    const/4 v1, 0x0

    invoke-direct {v0, p0, v1}, Lcom/helpshift/HelpshiftUser;-><init>(Lcom/helpshift/HelpshiftUser$Builder;Lcom/helpshift/HelpshiftUser$1;)V

    return-object v0
.end method

.method public setAuthToken(Ljava/lang/String;)Lcom/helpshift/HelpshiftUser$Builder;
    .locals 0

    .line 59
    iput-object p1, p0, Lcom/helpshift/HelpshiftUser$Builder;->authToken:Ljava/lang/String;

    return-object p0
.end method

.method public setName(Ljava/lang/String;)Lcom/helpshift/HelpshiftUser$Builder;
    .locals 0

    .line 54
    iput-object p1, p0, Lcom/helpshift/HelpshiftUser$Builder;->name:Ljava/lang/String;

    return-object p0
.end method
