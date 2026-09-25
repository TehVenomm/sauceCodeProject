.class public final Lnet/gogame/gowrap/support/DiskLruCache$Editor;
.super Ljava/lang/Object;
.source "DiskLruCache.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/gowrap/support/DiskLruCache;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x11
    name = "Editor"
.end annotation

.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lnet/gogame/gowrap/support/DiskLruCache$Editor$FaultHidingOutputStream;
    }
.end annotation


# instance fields
.field private final entry:Lnet/gogame/gowrap/support/DiskLruCache$Entry;

.field private hasErrors:Z

.field final synthetic this$0:Lnet/gogame/gowrap/support/DiskLruCache;


# direct methods
.method private constructor <init>(Lnet/gogame/gowrap/support/DiskLruCache;Lnet/gogame/gowrap/support/DiskLruCache$Entry;)V
    .locals 0

    .line 764
    iput-object p1, p0, Lnet/gogame/gowrap/support/DiskLruCache$Editor;->this$0:Lnet/gogame/gowrap/support/DiskLruCache;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 765
    iput-object p2, p0, Lnet/gogame/gowrap/support/DiskLruCache$Editor;->entry:Lnet/gogame/gowrap/support/DiskLruCache$Entry;

    return-void
.end method

.method synthetic constructor <init>(Lnet/gogame/gowrap/support/DiskLruCache;Lnet/gogame/gowrap/support/DiskLruCache$Entry;Lnet/gogame/gowrap/support/DiskLruCache$1;)V
    .locals 0

    .line 760
    invoke-direct {p0, p1, p2}, Lnet/gogame/gowrap/support/DiskLruCache$Editor;-><init>(Lnet/gogame/gowrap/support/DiskLruCache;Lnet/gogame/gowrap/support/DiskLruCache$Entry;)V

    return-void
.end method

.method static synthetic access$1400(Lnet/gogame/gowrap/support/DiskLruCache$Editor;)Lnet/gogame/gowrap/support/DiskLruCache$Entry;
    .locals 0

    .line 760
    iget-object p0, p0, Lnet/gogame/gowrap/support/DiskLruCache$Editor;->entry:Lnet/gogame/gowrap/support/DiskLruCache$Entry;

    return-object p0
.end method

.method static synthetic access$2002(Lnet/gogame/gowrap/support/DiskLruCache$Editor;Z)Z
    .locals 0

    .line 760
    iput-boolean p1, p0, Lnet/gogame/gowrap/support/DiskLruCache$Editor;->hasErrors:Z

    return p1
.end method


# virtual methods
.method public abort()V
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 840
    iget-object v0, p0, Lnet/gogame/gowrap/support/DiskLruCache$Editor;->this$0:Lnet/gogame/gowrap/support/DiskLruCache;

    const/4 v1, 0x0

    invoke-static {v0, p0, v1}, Lnet/gogame/gowrap/support/DiskLruCache;->access$1900(Lnet/gogame/gowrap/support/DiskLruCache;Lnet/gogame/gowrap/support/DiskLruCache$Editor;Z)V

    return-void
.end method

.method public commit()V
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 827
    iget-boolean v0, p0, Lnet/gogame/gowrap/support/DiskLruCache$Editor;->hasErrors:Z

    if-eqz v0, :cond_0

    .line 828
    iget-object v0, p0, Lnet/gogame/gowrap/support/DiskLruCache$Editor;->this$0:Lnet/gogame/gowrap/support/DiskLruCache;

    const/4 v1, 0x0

    invoke-static {v0, p0, v1}, Lnet/gogame/gowrap/support/DiskLruCache;->access$1900(Lnet/gogame/gowrap/support/DiskLruCache;Lnet/gogame/gowrap/support/DiskLruCache$Editor;Z)V

    .line 829
    iget-object v0, p0, Lnet/gogame/gowrap/support/DiskLruCache$Editor;->this$0:Lnet/gogame/gowrap/support/DiskLruCache;

    iget-object v1, p0, Lnet/gogame/gowrap/support/DiskLruCache$Editor;->entry:Lnet/gogame/gowrap/support/DiskLruCache$Entry;

    invoke-static {v1}, Lnet/gogame/gowrap/support/DiskLruCache$Entry;->access$1100(Lnet/gogame/gowrap/support/DiskLruCache$Entry;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Lnet/gogame/gowrap/support/DiskLruCache;->remove(Ljava/lang/String;)Z

    goto :goto_0

    .line 831
    :cond_0
    iget-object v0, p0, Lnet/gogame/gowrap/support/DiskLruCache$Editor;->this$0:Lnet/gogame/gowrap/support/DiskLruCache;

    const/4 v1, 0x1

    invoke-static {v0, p0, v1}, Lnet/gogame/gowrap/support/DiskLruCache;->access$1900(Lnet/gogame/gowrap/support/DiskLruCache;Lnet/gogame/gowrap/support/DiskLruCache$Editor;Z)V

    :goto_0
    return-void
.end method

.method public getString(I)Ljava/lang/String;
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 789
    invoke-virtual {p0, p1}, Lnet/gogame/gowrap/support/DiskLruCache$Editor;->newInputStream(I)Ljava/io/InputStream;

    move-result-object p1

    if-eqz p1, :cond_0

    .line 790
    invoke-static {p1}, Lnet/gogame/gowrap/support/DiskLruCache;->access$1600(Ljava/io/InputStream;)Ljava/lang/String;

    move-result-object p1

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    :goto_0
    return-object p1
.end method

.method public newInputStream(I)Ljava/io/InputStream;
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 773
    iget-object v0, p0, Lnet/gogame/gowrap/support/DiskLruCache$Editor;->this$0:Lnet/gogame/gowrap/support/DiskLruCache;

    monitor-enter v0

    .line 774
    :try_start_0
    iget-object v1, p0, Lnet/gogame/gowrap/support/DiskLruCache$Editor;->entry:Lnet/gogame/gowrap/support/DiskLruCache$Entry;

    invoke-static {v1}, Lnet/gogame/gowrap/support/DiskLruCache$Entry;->access$700(Lnet/gogame/gowrap/support/DiskLruCache$Entry;)Lnet/gogame/gowrap/support/DiskLruCache$Editor;

    move-result-object v1

    if-ne v1, p0, :cond_1

    .line 777
    iget-object v1, p0, Lnet/gogame/gowrap/support/DiskLruCache$Editor;->entry:Lnet/gogame/gowrap/support/DiskLruCache$Entry;

    invoke-static {v1}, Lnet/gogame/gowrap/support/DiskLruCache$Entry;->access$600(Lnet/gogame/gowrap/support/DiskLruCache$Entry;)Z

    move-result v1

    if-nez v1, :cond_0

    const/4 p1, 0x0

    .line 778
    monitor-exit v0

    return-object p1

    .line 780
    :cond_0
    new-instance v1, Ljava/io/FileInputStream;

    iget-object v2, p0, Lnet/gogame/gowrap/support/DiskLruCache$Editor;->entry:Lnet/gogame/gowrap/support/DiskLruCache$Entry;

    invoke-virtual {v2, p1}, Lnet/gogame/gowrap/support/DiskLruCache$Entry;->getCleanFile(I)Ljava/io/File;

    move-result-object p1

    invoke-direct {v1, p1}, Ljava/io/FileInputStream;-><init>(Ljava/io/File;)V

    monitor-exit v0

    return-object v1

    .line 775
    :cond_1
    new-instance p1, Ljava/lang/IllegalStateException;

    invoke-direct {p1}, Ljava/lang/IllegalStateException;-><init>()V

    throw p1

    :catchall_0
    move-exception p1

    .line 781
    monitor-exit v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw p1
.end method

.method public newOutputStream(I)Ljava/io/OutputStream;
    .locals 4
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 801
    iget-object v0, p0, Lnet/gogame/gowrap/support/DiskLruCache$Editor;->this$0:Lnet/gogame/gowrap/support/DiskLruCache;

    monitor-enter v0

    .line 802
    :try_start_0
    iget-object v1, p0, Lnet/gogame/gowrap/support/DiskLruCache$Editor;->entry:Lnet/gogame/gowrap/support/DiskLruCache$Entry;

    invoke-static {v1}, Lnet/gogame/gowrap/support/DiskLruCache$Entry;->access$700(Lnet/gogame/gowrap/support/DiskLruCache$Entry;)Lnet/gogame/gowrap/support/DiskLruCache$Editor;

    move-result-object v1

    if-ne v1, p0, :cond_0

    .line 805
    new-instance v1, Lnet/gogame/gowrap/support/DiskLruCache$Editor$FaultHidingOutputStream;

    new-instance v2, Ljava/io/FileOutputStream;

    iget-object v3, p0, Lnet/gogame/gowrap/support/DiskLruCache$Editor;->entry:Lnet/gogame/gowrap/support/DiskLruCache$Entry;

    invoke-virtual {v3, p1}, Lnet/gogame/gowrap/support/DiskLruCache$Entry;->getDirtyFile(I)Ljava/io/File;

    move-result-object p1

    invoke-direct {v2, p1}, Ljava/io/FileOutputStream;-><init>(Ljava/io/File;)V

    const/4 p1, 0x0

    invoke-direct {v1, p0, v2, p1}, Lnet/gogame/gowrap/support/DiskLruCache$Editor$FaultHidingOutputStream;-><init>(Lnet/gogame/gowrap/support/DiskLruCache$Editor;Ljava/io/OutputStream;Lnet/gogame/gowrap/support/DiskLruCache$1;)V

    monitor-exit v0

    return-object v1

    .line 803
    :cond_0
    new-instance p1, Ljava/lang/IllegalStateException;

    invoke-direct {p1}, Ljava/lang/IllegalStateException;-><init>()V

    throw p1

    :catchall_0
    move-exception p1

    .line 806
    monitor-exit v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw p1
.end method

.method public set(ILjava/lang/String;)V
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    const/4 v0, 0x0

    .line 815
    :try_start_0
    new-instance v1, Ljava/io/OutputStreamWriter;

    invoke-virtual {p0, p1}, Lnet/gogame/gowrap/support/DiskLruCache$Editor;->newOutputStream(I)Ljava/io/OutputStream;

    move-result-object p1

    invoke-static {}, Lnet/gogame/gowrap/support/DiskLruCache;->access$1800()Ljava/nio/charset/Charset;

    move-result-object v2

    invoke-direct {v1, p1, v2}, Ljava/io/OutputStreamWriter;-><init>(Ljava/io/OutputStream;Ljava/nio/charset/Charset;)V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_1

    .line 816
    :try_start_1
    invoke-virtual {v1, p2}, Ljava/io/Writer;->write(Ljava/lang/String;)V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 818
    invoke-static {v1}, Lnet/gogame/gowrap/support/DiskLruCache;->closeQuietly(Ljava/io/Closeable;)V

    return-void

    :catchall_0
    move-exception p1

    move-object v0, v1

    goto :goto_0

    :catchall_1
    move-exception p1

    :goto_0
    invoke-static {v0}, Lnet/gogame/gowrap/support/DiskLruCache;->closeQuietly(Ljava/io/Closeable;)V

    .line 819
    throw p1
.end method
