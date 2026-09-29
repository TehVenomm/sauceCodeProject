.class public Lcom/helpshift/HelpshiftUser;
.super Ljava/lang/Object;
.source "HelpshiftUser.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/HelpshiftUser$Builder;
    }
.end annotation


# instance fields
.field private authToken:Ljava/lang/String;

.field private email:Ljava/lang/String;

.field private identifier:Ljava/lang/String;

.field private name:Ljava/lang/String;


# direct methods
.method private constructor <init>(Lcom/helpshift/HelpshiftUser$Builder;)V
    .locals 1

    .line 15
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 16
    invoke-static {p1}, Lcom/helpshift/HelpshiftUser$Builder;->access$000(Lcom/helpshift/HelpshiftUser$Builder;)Ljava/lang/String;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/HelpshiftUser;->identifier:Ljava/lang/String;

    .line 17
    invoke-static {p1}, Lcom/helpshift/HelpshiftUser$Builder;->access$100(Lcom/helpshift/HelpshiftUser$Builder;)Ljava/lang/String;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/HelpshiftUser;->email:Ljava/lang/String;

    .line 18
    invoke-static {p1}, Lcom/helpshift/HelpshiftUser$Builder;->access$200(Lcom/helpshift/HelpshiftUser$Builder;)Ljava/lang/String;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/HelpshiftUser;->name:Ljava/lang/String;

    .line 19
    invoke-static {p1}, Lcom/helpshift/HelpshiftUser$Builder;->access$300(Lcom/helpshift/HelpshiftUser$Builder;)Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/HelpshiftUser;->authToken:Ljava/lang/String;

    return-void
.end method

.method synthetic constructor <init>(Lcom/helpshift/HelpshiftUser$Builder;Lcom/helpshift/HelpshiftUser$1;)V
    .locals 0

    .line 9
    invoke-direct {p0, p1}, Lcom/helpshift/HelpshiftUser;-><init>(Lcom/helpshift/HelpshiftUser$Builder;)V

    return-void
.end method


# virtual methods
.method public getAuthToken()Ljava/lang/String;
    .locals 1

    .line 35
    iget-object v0, p0, Lcom/helpshift/HelpshiftUser;->authToken:Ljava/lang/String;

    return-object v0
.end method

.method public getEmail()Ljava/lang/String;
    .locals 1

    .line 27
    iget-object v0, p0, Lcom/helpshift/HelpshiftUser;->email:Ljava/lang/String;

    return-object v0
.end method

.method public getIdentifier()Ljava/lang/String;
    .locals 1

    .line 23
    iget-object v0, p0, Lcom/helpshift/HelpshiftUser;->identifier:Ljava/lang/String;

    return-object v0
.end method

.method public getName()Ljava/lang/String;
    .locals 1

    .line 31
    iget-object v0, p0, Lcom/helpshift/HelpshiftUser;->name:Ljava/lang/String;

    return-object v0
.end method
