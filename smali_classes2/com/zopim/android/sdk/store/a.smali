.class abstract Lcom/zopim/android/sdk/store/a;
.super Ljava/lang/Object;

# interfaces
.implements Lcom/zopim/android/sdk/store/BaseStorage;


# static fields
.field private static final DEFAULT_PREFS_NAME:Ljava/lang/String; = "zopim_chat"


# instance fields
.field protected mDisabled:Z

.field protected final mStoragePreferences:Landroid/content/SharedPreferences;


# direct methods
.method private constructor <init>()V
    .locals 2

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    new-instance v0, Ljava/lang/UnsupportedOperationException;

    const-string v1, "Use of unsupported constructor"

    invoke-direct {v0, v1}, Ljava/lang/UnsupportedOperationException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method protected constructor <init>(Landroid/content/Context;)V
    .locals 1

    const/4 v0, 0x0

    invoke-direct {p0, p1, v0}, Lcom/zopim/android/sdk/store/a;-><init>(Landroid/content/Context;Ljava/lang/String;)V

    return-void
.end method

.method protected constructor <init>(Landroid/content/Context;Ljava/lang/String;)V
    .locals 1

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    if-eqz p1, :cond_1

    invoke-virtual {p1}, Landroid/content/Context;->getApplicationContext()Landroid/content/Context;

    move-result-object p1

    const/4 v0, 0x0

    if-nez p2, :cond_0

    const-string p2, "zopim_chat"

    :cond_0
    invoke-virtual {p1, p2, v0}, Landroid/content/Context;->getSharedPreferences(Ljava/lang/String;I)Landroid/content/SharedPreferences;

    move-result-object p1

    iput-object p1, p0, Lcom/zopim/android/sdk/store/a;->mStoragePreferences:Landroid/content/SharedPreferences;

    return-void

    :cond_1
    new-instance p1, Ljava/lang/IllegalArgumentException;

    const-string p2, "Context must not be null"

    invoke-direct {p1, p2}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p1
.end method


# virtual methods
.method public delete()V
    .locals 1

    iget-object v0, p0, Lcom/zopim/android/sdk/store/a;->mStoragePreferences:Landroid/content/SharedPreferences;

    invoke-interface {v0}, Landroid/content/SharedPreferences;->edit()Landroid/content/SharedPreferences$Editor;

    move-result-object v0

    invoke-interface {v0}, Landroid/content/SharedPreferences$Editor;->clear()Landroid/content/SharedPreferences$Editor;

    move-result-object v0

    invoke-interface {v0}, Landroid/content/SharedPreferences$Editor;->apply()V

    return-void
.end method

.method public disable()V
    .locals 1

    invoke-virtual {p0}, Lcom/zopim/android/sdk/store/a;->delete()V

    const/4 v0, 0x1

    iput-boolean v0, p0, Lcom/zopim/android/sdk/store/a;->mDisabled:Z

    return-void
.end method
