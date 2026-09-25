.class Lcom/zopim/android/sdk/api/FileTransfers$a;
.super Ljava/lang/Object;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/zopim/android/sdk/api/FileTransfers;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = "a"
.end annotation


# instance fields
.field public a:Ljava/io/File;

.field public b:Lcom/zopim/android/sdk/api/FileTransfers$b;


# direct methods
.method constructor <init>()V
    .locals 1

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    sget-object v0, Lcom/zopim/android/sdk/api/FileTransfers$b;->a:Lcom/zopim/android/sdk/api/FileTransfers$b;

    iput-object v0, p0, Lcom/zopim/android/sdk/api/FileTransfers$a;->b:Lcom/zopim/android/sdk/api/FileTransfers$b;

    return-void
.end method
