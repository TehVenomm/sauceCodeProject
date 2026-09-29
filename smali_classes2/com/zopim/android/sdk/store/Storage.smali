.class public final enum Lcom/zopim/android/sdk/store/Storage;
.super Ljava/lang/Enum;


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Enum<",
        "Lcom/zopim/android/sdk/store/Storage;",
        ">;"
    }
.end annotation


# static fields
.field private static final synthetic $VALUES:[Lcom/zopim/android/sdk/store/Storage;

.field public static final enum INSTANCE:Lcom/zopim/android/sdk/store/Storage;

.field private static final LOG_TAG:Ljava/lang/String;


# instance fields
.field private mAppContext:Landroid/content/Context;


# direct methods
.method static constructor <clinit>()V
    .locals 3

    new-instance v0, Lcom/zopim/android/sdk/store/Storage;

    const-string v1, "INSTANCE"

    const/4 v2, 0x0

    invoke-direct {v0, v1, v2}, Lcom/zopim/android/sdk/store/Storage;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/zopim/android/sdk/store/Storage;->INSTANCE:Lcom/zopim/android/sdk/store/Storage;

    const/4 v0, 0x1

    new-array v0, v0, [Lcom/zopim/android/sdk/store/Storage;

    sget-object v1, Lcom/zopim/android/sdk/store/Storage;->INSTANCE:Lcom/zopim/android/sdk/store/Storage;

    aput-object v1, v0, v2

    sput-object v0, Lcom/zopim/android/sdk/store/Storage;->$VALUES:[Lcom/zopim/android/sdk/store/Storage;

    const-class v0, Lcom/zopim/android/sdk/store/Storage;

    invoke-virtual {v0}, Ljava/lang/Class;->getSimpleName()Ljava/lang/String;

    move-result-object v0

    sput-object v0, Lcom/zopim/android/sdk/store/Storage;->LOG_TAG:Ljava/lang/String;

    return-void
.end method

.method private constructor <init>(Ljava/lang/String;I)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()V"
        }
    .end annotation

    invoke-direct {p0, p1, p2}, Ljava/lang/Enum;-><init>(Ljava/lang/String;I)V

    return-void
.end method

.method public static init(Landroid/content/Context;)V
    .locals 1

    if-nez p0, :cond_0

    sget-object p0, Lcom/zopim/android/sdk/store/Storage;->LOG_TAG:Ljava/lang/String;

    const-string v0, "Can not initialize storage. Context must not be null."

    invoke-static {p0, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    return-void

    :cond_0
    sget-object v0, Lcom/zopim/android/sdk/store/Storage;->INSTANCE:Lcom/zopim/android/sdk/store/Storage;

    invoke-virtual {p0}, Landroid/content/Context;->getApplicationContext()Landroid/content/Context;

    move-result-object p0

    iput-object p0, v0, Lcom/zopim/android/sdk/store/Storage;->mAppContext:Landroid/content/Context;

    return-void
.end method

.method private isInitialized()Z
    .locals 1

    iget-object v0, p0, Lcom/zopim/android/sdk/store/Storage;->mAppContext:Landroid/content/Context;

    if-eqz v0, :cond_0

    const/4 v0, 0x1

    return v0

    :cond_0
    const/4 v0, 0x0

    return v0
.end method

.method public static machineId()Lcom/zopim/android/sdk/store/MachineIdStorage;
    .locals 2

    sget-object v0, Lcom/zopim/android/sdk/store/Storage;->INSTANCE:Lcom/zopim/android/sdk/store/Storage;

    invoke-direct {v0}, Lcom/zopim/android/sdk/store/Storage;->isInitialized()Z

    move-result v0

    if-eqz v0, :cond_0

    new-instance v0, Lcom/zopim/android/sdk/store/MachineIdPrefsStorage;

    sget-object v1, Lcom/zopim/android/sdk/store/Storage;->INSTANCE:Lcom/zopim/android/sdk/store/Storage;

    iget-object v1, v1, Lcom/zopim/android/sdk/store/Storage;->mAppContext:Landroid/content/Context;

    invoke-direct {v0, v1}, Lcom/zopim/android/sdk/store/MachineIdPrefsStorage;-><init>(Landroid/content/Context;)V

    return-object v0

    :cond_0
    sget-object v0, Lcom/zopim/android/sdk/store/Storage;->LOG_TAG:Ljava/lang/String;

    const-string v1, "Storage must be initialized first. Will return mocked storage implementation."

    invoke-static {v0, v1}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    new-instance v0, Lcom/zopim/android/sdk/store/b;

    invoke-direct {v0}, Lcom/zopim/android/sdk/store/b;-><init>()V

    return-object v0
.end method

.method public static valueOf(Ljava/lang/String;)Lcom/zopim/android/sdk/store/Storage;
    .locals 1

    const-class v0, Lcom/zopim/android/sdk/store/Storage;

    invoke-static {v0, p0}, Ljava/lang/Enum;->valueOf(Ljava/lang/Class;Ljava/lang/String;)Ljava/lang/Enum;

    move-result-object p0

    check-cast p0, Lcom/zopim/android/sdk/store/Storage;

    return-object p0
.end method

.method public static values()[Lcom/zopim/android/sdk/store/Storage;
    .locals 1

    sget-object v0, Lcom/zopim/android/sdk/store/Storage;->$VALUES:[Lcom/zopim/android/sdk/store/Storage;

    invoke-virtual {v0}, [Lcom/zopim/android/sdk/store/Storage;->clone()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [Lcom/zopim/android/sdk/store/Storage;

    return-object v0
.end method

.method public static visitorInfo()Lcom/zopim/android/sdk/store/VisitorInfoStorage;
    .locals 2

    sget-object v0, Lcom/zopim/android/sdk/store/Storage;->INSTANCE:Lcom/zopim/android/sdk/store/Storage;

    invoke-direct {v0}, Lcom/zopim/android/sdk/store/Storage;->isInitialized()Z

    move-result v0

    if-eqz v0, :cond_0

    new-instance v0, Lcom/zopim/android/sdk/store/VisitorInfoPrefsStorage;

    sget-object v1, Lcom/zopim/android/sdk/store/Storage;->INSTANCE:Lcom/zopim/android/sdk/store/Storage;

    iget-object v1, v1, Lcom/zopim/android/sdk/store/Storage;->mAppContext:Landroid/content/Context;

    invoke-direct {v0, v1}, Lcom/zopim/android/sdk/store/VisitorInfoPrefsStorage;-><init>(Landroid/content/Context;)V

    return-object v0

    :cond_0
    sget-object v0, Lcom/zopim/android/sdk/store/Storage;->LOG_TAG:Ljava/lang/String;

    const-string v1, "Storage must be initialized first. Will return dummy storage implementation."

    invoke-static {v0, v1}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    new-instance v0, Lcom/zopim/android/sdk/store/c;

    invoke-direct {v0}, Lcom/zopim/android/sdk/store/c;-><init>()V

    return-object v0
.end method


# virtual methods
.method public clearAll()V
    .locals 1

    invoke-static {}, Lcom/zopim/android/sdk/store/Storage;->machineId()Lcom/zopim/android/sdk/store/MachineIdStorage;

    move-result-object v0

    invoke-interface {v0}, Lcom/zopim/android/sdk/store/MachineIdStorage;->delete()V

    invoke-static {}, Lcom/zopim/android/sdk/store/Storage;->visitorInfo()Lcom/zopim/android/sdk/store/VisitorInfoStorage;

    move-result-object v0

    invoke-interface {v0}, Lcom/zopim/android/sdk/store/VisitorInfoStorage;->delete()V

    return-void
.end method
