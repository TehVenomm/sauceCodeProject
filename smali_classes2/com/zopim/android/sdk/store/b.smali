.class final Lcom/zopim/android/sdk/store/b;
.super Ljava/lang/Object;

# interfaces
.implements Lcom/zopim/android/sdk/store/MachineIdStorage;


# static fields
.field private static final a:Ljava/lang/String; = "b"


# direct methods
.method static constructor <clinit>()V
    .locals 0

    return-void
.end method

.method constructor <init>()V
    .locals 0

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public delete()V
    .locals 2

    sget-object v0, Lcom/zopim/android/sdk/store/b;->a:Ljava/lang/String;

    const-string v1, "Storage is not initialized. Skipping operation."

    invoke-static {v0, v1}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    return-void
.end method

.method public disable()V
    .locals 2

    sget-object v0, Lcom/zopim/android/sdk/store/b;->a:Ljava/lang/String;

    const-string v1, "Storage is not initialized. Skipping operation."

    invoke-static {v0, v1}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    return-void
.end method

.method public getMachineId()Ljava/lang/String;
    .locals 2

    sget-object v0, Lcom/zopim/android/sdk/store/b;->a:Ljava/lang/String;

    const-string v1, "Storage is not initialized. Skipping operation. Will return empty string"

    invoke-static {v0, v1}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    const-string v0, ""

    return-object v0
.end method

.method public setMachineId(Ljava/lang/String;)V
    .locals 1

    sget-object p1, Lcom/zopim/android/sdk/store/b;->a:Ljava/lang/String;

    const-string v0, "Storage is not initialized. Skipping operation."

    invoke-static {p1, v0}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    return-void
.end method
