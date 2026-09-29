.class public Lcom/helpshift/providers/CrossModuleDataProvider;
.super Ljava/lang/Object;
.source "CrossModuleDataProvider.java"


# static fields
.field private static campaignsModuleAPIs:Lcom/helpshift/providers/ICampaignsModuleAPIs;

.field private static supportDataProvider:Lcom/helpshift/providers/ISupportDataProvider;


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 7
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static getCampaignModuleAPIs()Lcom/helpshift/providers/ICampaignsModuleAPIs;
    .locals 1

    .line 19
    sget-object v0, Lcom/helpshift/providers/CrossModuleDataProvider;->campaignsModuleAPIs:Lcom/helpshift/providers/ICampaignsModuleAPIs;

    return-object v0
.end method

.method public static getSupportDataProvider()Lcom/helpshift/providers/ISupportDataProvider;
    .locals 1

    .line 23
    sget-object v0, Lcom/helpshift/providers/CrossModuleDataProvider;->supportDataProvider:Lcom/helpshift/providers/ISupportDataProvider;

    return-object v0
.end method

.method public static setCampaignsDataProvider(Lcom/helpshift/providers/ICampaignsModuleAPIs;)V
    .locals 0

    .line 15
    sput-object p0, Lcom/helpshift/providers/CrossModuleDataProvider;->campaignsModuleAPIs:Lcom/helpshift/providers/ICampaignsModuleAPIs;

    return-void
.end method

.method public static setSupportDataProvider(Lcom/helpshift/providers/ISupportDataProvider;)V
    .locals 0

    .line 27
    sput-object p0, Lcom/helpshift/providers/CrossModuleDataProvider;->supportDataProvider:Lcom/helpshift/providers/ISupportDataProvider;

    return-void
.end method
