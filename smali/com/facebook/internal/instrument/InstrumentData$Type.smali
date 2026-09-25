.class public final enum Lcom/facebook/internal/instrument/InstrumentData$Type;
.super Ljava/lang/Enum;
.source "InstrumentData.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/facebook/internal/instrument/InstrumentData;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x4019
    name = "Type"
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Enum<",
        "Lcom/facebook/internal/instrument/InstrumentData$Type;",
        ">;"
    }
.end annotation


# static fields
.field private static final synthetic $VALUES:[Lcom/facebook/internal/instrument/InstrumentData$Type;

.field public static final enum Analysis:Lcom/facebook/internal/instrument/InstrumentData$Type;

.field public static final enum CrashReport:Lcom/facebook/internal/instrument/InstrumentData$Type;

.field public static final enum CrashShield:Lcom/facebook/internal/instrument/InstrumentData$Type;

.field public static final enum ThreadCheck:Lcom/facebook/internal/instrument/InstrumentData$Type;

.field public static final enum Unknown:Lcom/facebook/internal/instrument/InstrumentData$Type;


# direct methods
.method static constructor <clinit>()V
    .locals 7

    .line 39
    new-instance v0, Lcom/facebook/internal/instrument/InstrumentData$Type;

    const-string v1, "Unknown"

    const/4 v2, 0x0

    invoke-direct {v0, v1, v2}, Lcom/facebook/internal/instrument/InstrumentData$Type;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/facebook/internal/instrument/InstrumentData$Type;->Unknown:Lcom/facebook/internal/instrument/InstrumentData$Type;

    .line 40
    new-instance v0, Lcom/facebook/internal/instrument/InstrumentData$Type;

    const-string v1, "Analysis"

    const/4 v3, 0x1

    invoke-direct {v0, v1, v3}, Lcom/facebook/internal/instrument/InstrumentData$Type;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/facebook/internal/instrument/InstrumentData$Type;->Analysis:Lcom/facebook/internal/instrument/InstrumentData$Type;

    .line 41
    new-instance v0, Lcom/facebook/internal/instrument/InstrumentData$Type;

    const-string v1, "CrashReport"

    const/4 v4, 0x2

    invoke-direct {v0, v1, v4}, Lcom/facebook/internal/instrument/InstrumentData$Type;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/facebook/internal/instrument/InstrumentData$Type;->CrashReport:Lcom/facebook/internal/instrument/InstrumentData$Type;

    .line 42
    new-instance v0, Lcom/facebook/internal/instrument/InstrumentData$Type;

    const-string v1, "CrashShield"

    const/4 v5, 0x3

    invoke-direct {v0, v1, v5}, Lcom/facebook/internal/instrument/InstrumentData$Type;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/facebook/internal/instrument/InstrumentData$Type;->CrashShield:Lcom/facebook/internal/instrument/InstrumentData$Type;

    .line 43
    new-instance v0, Lcom/facebook/internal/instrument/InstrumentData$Type;

    const-string v1, "ThreadCheck"

    const/4 v6, 0x4

    invoke-direct {v0, v1, v6}, Lcom/facebook/internal/instrument/InstrumentData$Type;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/facebook/internal/instrument/InstrumentData$Type;->ThreadCheck:Lcom/facebook/internal/instrument/InstrumentData$Type;

    const/4 v0, 0x5

    .line 38
    new-array v0, v0, [Lcom/facebook/internal/instrument/InstrumentData$Type;

    sget-object v1, Lcom/facebook/internal/instrument/InstrumentData$Type;->Unknown:Lcom/facebook/internal/instrument/InstrumentData$Type;

    aput-object v1, v0, v2

    sget-object v1, Lcom/facebook/internal/instrument/InstrumentData$Type;->Analysis:Lcom/facebook/internal/instrument/InstrumentData$Type;

    aput-object v1, v0, v3

    sget-object v1, Lcom/facebook/internal/instrument/InstrumentData$Type;->CrashReport:Lcom/facebook/internal/instrument/InstrumentData$Type;

    aput-object v1, v0, v4

    sget-object v1, Lcom/facebook/internal/instrument/InstrumentData$Type;->CrashShield:Lcom/facebook/internal/instrument/InstrumentData$Type;

    aput-object v1, v0, v5

    sget-object v1, Lcom/facebook/internal/instrument/InstrumentData$Type;->ThreadCheck:Lcom/facebook/internal/instrument/InstrumentData$Type;

    aput-object v1, v0, v6

    sput-object v0, Lcom/facebook/internal/instrument/InstrumentData$Type;->$VALUES:[Lcom/facebook/internal/instrument/InstrumentData$Type;

    return-void
.end method

.method private constructor <init>(Ljava/lang/String;I)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()V"
        }
    .end annotation

    .line 38
    invoke-direct {p0, p1, p2}, Ljava/lang/Enum;-><init>(Ljava/lang/String;I)V

    return-void
.end method

.method public static valueOf(Ljava/lang/String;)Lcom/facebook/internal/instrument/InstrumentData$Type;
    .locals 1

    .line 38
    const-class v0, Lcom/facebook/internal/instrument/InstrumentData$Type;

    invoke-static {v0, p0}, Ljava/lang/Enum;->valueOf(Ljava/lang/Class;Ljava/lang/String;)Ljava/lang/Enum;

    move-result-object p0

    check-cast p0, Lcom/facebook/internal/instrument/InstrumentData$Type;

    return-object p0
.end method

.method public static values()[Lcom/facebook/internal/instrument/InstrumentData$Type;
    .locals 1

    .line 38
    sget-object v0, Lcom/facebook/internal/instrument/InstrumentData$Type;->$VALUES:[Lcom/facebook/internal/instrument/InstrumentData$Type;

    invoke-virtual {v0}, [Lcom/facebook/internal/instrument/InstrumentData$Type;->clone()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [Lcom/facebook/internal/instrument/InstrumentData$Type;

    return-object v0
.end method


# virtual methods
.method public toString()Ljava/lang/String;
    .locals 2

    .line 47
    sget-object v0, Lcom/facebook/internal/instrument/InstrumentData$1;->$SwitchMap$com$facebook$internal$instrument$InstrumentData$Type:[I

    invoke-virtual {p0}, Lcom/facebook/internal/instrument/InstrumentData$Type;->ordinal()I

    move-result v1

    aget v0, v0, v1

    packed-switch v0, :pswitch_data_0

    const-string v0, "Unknown"

    return-object v0

    :pswitch_0
    const-string v0, "ThreadCheck"

    return-object v0

    :pswitch_1
    const-string v0, "CrashShield"

    return-object v0

    :pswitch_2
    const-string v0, "CrashReport"

    return-object v0

    :pswitch_3
    const-string v0, "Analysis"

    return-object v0

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_3
        :pswitch_2
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method
