.class public final enum Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;
.super Ljava/lang/Enum;
.source "AutoRetryFailedEventDM.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/common/AutoRetryFailedEventDM;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x4019
    name = "EventType"
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Enum<",
        "Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;",
        ">;"
    }
.end annotation


# static fields
.field private static final synthetic $VALUES:[Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

.field public static final enum ANALYTICS:Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

.field public static final enum CLEAR_USER:Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

.field public static final enum CONVERSATION:Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

.field public static final enum FAQ:Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

.field public static final enum MIGRATION:Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

.field public static final enum PUSH_TOKEN:Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

.field public static final enum SYNC_USER:Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;


# direct methods
.method static constructor <clinit>()V
    .locals 9

    .line 211
    new-instance v0, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

    const-string v1, "MIGRATION"

    const/4 v2, 0x0

    invoke-direct {v0, v1, v2}, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;->MIGRATION:Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

    .line 212
    new-instance v0, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

    const-string v1, "SYNC_USER"

    const/4 v3, 0x1

    invoke-direct {v0, v1, v3}, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;->SYNC_USER:Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

    .line 213
    new-instance v0, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

    const-string v1, "PUSH_TOKEN"

    const/4 v4, 0x2

    invoke-direct {v0, v1, v4}, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;->PUSH_TOKEN:Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

    .line 214
    new-instance v0, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

    const-string v1, "CLEAR_USER"

    const/4 v5, 0x3

    invoke-direct {v0, v1, v5}, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;->CLEAR_USER:Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

    .line 215
    new-instance v0, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

    const-string v1, "CONVERSATION"

    const/4 v6, 0x4

    invoke-direct {v0, v1, v6}, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;->CONVERSATION:Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

    .line 216
    new-instance v0, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

    const-string v1, "FAQ"

    const/4 v7, 0x5

    invoke-direct {v0, v1, v7}, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;->FAQ:Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

    .line 217
    new-instance v0, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

    const-string v1, "ANALYTICS"

    const/4 v8, 0x6

    invoke-direct {v0, v1, v8}, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;->ANALYTICS:Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

    const/4 v0, 0x7

    .line 210
    new-array v0, v0, [Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

    sget-object v1, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;->MIGRATION:Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

    aput-object v1, v0, v2

    sget-object v1, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;->SYNC_USER:Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

    aput-object v1, v0, v3

    sget-object v1, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;->PUSH_TOKEN:Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

    aput-object v1, v0, v4

    sget-object v1, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;->CLEAR_USER:Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

    aput-object v1, v0, v5

    sget-object v1, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;->CONVERSATION:Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

    aput-object v1, v0, v6

    sget-object v1, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;->FAQ:Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

    aput-object v1, v0, v7

    sget-object v1, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;->ANALYTICS:Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

    aput-object v1, v0, v8

    sput-object v0, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;->$VALUES:[Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

    return-void
.end method

.method private constructor <init>(Ljava/lang/String;I)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()V"
        }
    .end annotation

    .line 210
    invoke-direct {p0, p1, p2}, Ljava/lang/Enum;-><init>(Ljava/lang/String;I)V

    return-void
.end method

.method public static valueOf(Ljava/lang/String;)Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;
    .locals 1

    .line 210
    const-class v0, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

    invoke-static {v0, p0}, Ljava/lang/Enum;->valueOf(Ljava/lang/Class;Ljava/lang/String;)Ljava/lang/Enum;

    move-result-object p0

    check-cast p0, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

    return-object p0
.end method

.method public static values()[Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;
    .locals 1

    .line 210
    sget-object v0, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;->$VALUES:[Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

    invoke-virtual {v0}, [Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;->clone()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

    return-object v0
.end method
