.class public Lcom/github/droidfu/testsupport/DroidFuAssertions;
.super Ljava/lang/Object;
.source "DroidFuAssertions.java"


# static fields
.field private static junit4:Z = true


# direct methods
.method static constructor <clinit>()V
    .locals 2

    :try_start_0
    const-string v0, "org.junit.Assert"

    .line 13
    invoke-static {v0}, Ljava/lang/Class;->forName(Ljava/lang/String;)Ljava/lang/Class;
    :try_end_0
    .catch Ljava/lang/ClassNotFoundException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    .line 15
    :catch_0
    sget-object v0, Ljava/lang/System;->out:Ljava/io/PrintStream;

    const-string v1, "JUnit4 not found on class path, will use JUnit3 assertions!"

    invoke-virtual {v0, v1}, Ljava/io/PrintStream;->println(Ljava/lang/String;)V

    const/4 v0, 0x0

    .line 16
    sput-boolean v0, Lcom/github/droidfu/testsupport/DroidFuAssertions;->junit4:Z

    :goto_0
    return-void
.end method

.method public constructor <init>()V
    .locals 0

    .line 7
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static assertDateEquals(Ljava/util/Date;Ljava/util/Date;)V
    .locals 4

    .line 55
    invoke-static {}, Ljava/util/Calendar;->getInstance()Ljava/util/Calendar;

    move-result-object v0

    .line 56
    invoke-virtual {v0, p0}, Ljava/util/Calendar;->setTime(Ljava/util/Date;)V

    .line 57
    invoke-static {}, Ljava/util/Calendar;->getInstance()Ljava/util/Calendar;

    move-result-object p0

    .line 58
    invoke-virtual {p0, p1}, Ljava/util/Calendar;->setTime(Ljava/util/Date;)V

    const/4 p1, 0x1

    .line 60
    invoke-virtual {v0, p1}, Ljava/util/Calendar;->get(I)I

    move-result v1

    .line 61
    invoke-virtual {p0, p1}, Ljava/util/Calendar;->get(I)I

    move-result p1

    .line 62
    new-instance v2, Ljava/lang/StringBuilder;

    const-string v3, "expected year to be "

    invoke-direct {v2, v3}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    invoke-virtual {v2, v1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v3, ", but was "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2, p1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    .line 63
    invoke-static {v1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v1

    invoke-static {p1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p1

    .line 62
    invoke-static {v2, v1, p1}, Lcom/github/droidfu/testsupport/DroidFuAssertions;->assertEquals(Ljava/lang/String;Ljava/lang/Object;Ljava/lang/Object;)V

    const/4 p1, 0x2

    .line 65
    invoke-virtual {v0, p1}, Ljava/util/Calendar;->get(I)I

    move-result v1

    .line 66
    invoke-virtual {p0, p1}, Ljava/util/Calendar;->get(I)I

    move-result p1

    .line 67
    new-instance v2, Ljava/lang/StringBuilder;

    const-string v3, "expected month to be "

    invoke-direct {v2, v3}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    invoke-virtual {v2, v1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v3, ", but was "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2, p1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    .line 68
    invoke-static {v1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v1

    invoke-static {p1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p1

    .line 67
    invoke-static {v2, v1, p1}, Lcom/github/droidfu/testsupport/DroidFuAssertions;->assertEquals(Ljava/lang/String;Ljava/lang/Object;Ljava/lang/Object;)V

    const/4 p1, 0x5

    .line 70
    invoke-virtual {v0, p1}, Ljava/util/Calendar;->get(I)I

    move-result v0

    .line 71
    invoke-virtual {p0, p1}, Ljava/util/Calendar;->get(I)I

    move-result p0

    .line 72
    new-instance p1, Ljava/lang/StringBuilder;

    const-string v1, "expected day to be "

    invoke-direct {p1, v1}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v1, ", but was "

    invoke-virtual {p1, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1, p0}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-static {v0}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v0

    .line 73
    invoke-static {p0}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p0

    .line 72
    invoke-static {p1, v0, p0}, Lcom/github/droidfu/testsupport/DroidFuAssertions;->assertEquals(Ljava/lang/String;Ljava/lang/Object;Ljava/lang/Object;)V

    return-void
.end method

.method public static assertEqualElements(Ljava/util/Collection;Ljava/util/Collection;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "<E:",
            "Ljava/lang/Object;",
            ">(",
            "Ljava/util/Collection<",
            "TE;>;",
            "Ljava/util/Collection<",
            "TE;>;)V"
        }
    .end annotation

    .line 41
    invoke-interface {p0, p1}, Ljava/util/Collection;->removeAll(Ljava/util/Collection;)Z

    const-string p1, "collections expected to contain the same elements, but didn\'t"

    .line 43
    invoke-interface {p0}, Ljava/util/Collection;->isEmpty()Z

    move-result p0

    .line 42
    invoke-static {p1, p0}, Lcom/github/droidfu/testsupport/DroidFuAssertions;->assertTrue(Ljava/lang/String;Z)V

    return-void
.end method

.method private static assertEquals(Ljava/lang/String;Ljava/lang/Object;Ljava/lang/Object;)V
    .locals 1

    .line 33
    sget-boolean v0, Lcom/github/droidfu/testsupport/DroidFuAssertions;->junit4:Z

    if-eqz v0, :cond_0

    .line 34
    invoke-static {p0, p1, p2}, Lorg/junit/Assert;->assertEquals(Ljava/lang/String;Ljava/lang/Object;Ljava/lang/Object;)V

    goto :goto_0

    .line 36
    :cond_0
    invoke-static {p0, p1, p2}, Ljunit/framework/Assert;->assertEquals(Ljava/lang/String;Ljava/lang/Object;Ljava/lang/Object;)V

    :goto_0
    return-void
.end method

.method public static assertTimeEquals(Ljava/util/Date;Ljava/util/Date;)V
    .locals 4

    .line 85
    invoke-static {p0, p1}, Lcom/github/droidfu/testsupport/DroidFuAssertions;->assertDateEquals(Ljava/util/Date;Ljava/util/Date;)V

    .line 87
    invoke-static {}, Ljava/util/Calendar;->getInstance()Ljava/util/Calendar;

    move-result-object v0

    .line 88
    invoke-virtual {v0, p0}, Ljava/util/Calendar;->setTime(Ljava/util/Date;)V

    .line 89
    invoke-static {}, Ljava/util/Calendar;->getInstance()Ljava/util/Calendar;

    move-result-object p0

    .line 90
    invoke-virtual {p0, p1}, Ljava/util/Calendar;->setTime(Ljava/util/Date;)V

    const/16 p1, 0xb

    .line 92
    invoke-virtual {v0, p1}, Ljava/util/Calendar;->get(I)I

    move-result v1

    .line 93
    invoke-virtual {p0, p1}, Ljava/util/Calendar;->get(I)I

    move-result p1

    .line 94
    new-instance v2, Ljava/lang/StringBuilder;

    const-string v3, "expected hour to be "

    invoke-direct {v2, v3}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    invoke-virtual {v2, v1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v3, ", but was "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2, p1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    .line 95
    invoke-static {v1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v1

    invoke-static {p1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p1

    .line 94
    invoke-static {v2, v1, p1}, Lcom/github/droidfu/testsupport/DroidFuAssertions;->assertEquals(Ljava/lang/String;Ljava/lang/Object;Ljava/lang/Object;)V

    const/16 p1, 0xc

    .line 97
    invoke-virtual {v0, p1}, Ljava/util/Calendar;->get(I)I

    move-result v1

    .line 98
    invoke-virtual {p0, p1}, Ljava/util/Calendar;->get(I)I

    move-result p1

    .line 99
    new-instance v2, Ljava/lang/StringBuilder;

    const-string v3, "expected minute to be "

    invoke-direct {v2, v3}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    invoke-virtual {v2, v1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v3, ", but was "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2, p1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    .line 100
    invoke-static {v1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v1

    invoke-static {p1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p1

    .line 99
    invoke-static {v2, v1, p1}, Lcom/github/droidfu/testsupport/DroidFuAssertions;->assertEquals(Ljava/lang/String;Ljava/lang/Object;Ljava/lang/Object;)V

    const/16 p1, 0xd

    .line 102
    invoke-virtual {v0, p1}, Ljava/util/Calendar;->get(I)I

    move-result v0

    .line 103
    invoke-virtual {p0, p1}, Ljava/util/Calendar;->get(I)I

    move-result p0

    .line 104
    new-instance p1, Ljava/lang/StringBuilder;

    const-string v1, "expected second to be "

    invoke-direct {p1, v1}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v1, ", but was "

    invoke-virtual {p1, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1, p0}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    .line 105
    invoke-static {v0}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v0

    invoke-static {p0}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p0

    .line 104
    invoke-static {p1, v0, p0}, Lcom/github/droidfu/testsupport/DroidFuAssertions;->assertEquals(Ljava/lang/String;Ljava/lang/Object;Ljava/lang/Object;)V

    return-void
.end method

.method private static assertTrue(Ljava/lang/String;Z)V
    .locals 1

    .line 25
    sget-boolean v0, Lcom/github/droidfu/testsupport/DroidFuAssertions;->junit4:Z

    if-eqz v0, :cond_0

    .line 26
    invoke-static {p0, p1}, Lorg/junit/Assert;->assertTrue(Ljava/lang/String;Z)V

    goto :goto_0

    .line 28
    :cond_0
    invoke-static {p0, p1}, Ljunit/framework/Assert;->assertTrue(Ljava/lang/String;Z)V

    :goto_0
    return-void
.end method

.method public static useJUnit3()V
    .locals 1

    const/4 v0, 0x0

    .line 21
    sput-boolean v0, Lcom/github/droidfu/testsupport/DroidFuAssertions;->junit4:Z

    return-void
.end method
